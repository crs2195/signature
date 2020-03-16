using log4net;
using SBCustomCertStorage;
using SBHTTPSClient;
using SBHTTPTSPClient;
using SBPAdES;
using SBPDF;
using SBX509;
using System;
using System.IO;

namespace RemoteSignatureApp.Classes
{
    public class BlackBoxSignature
    {
        CSC_API_Client cscClient = new CSC_API_Client();
        private TElPDFAdvancedPublicKeySecurityHandler m_Handler = new TElPDFAdvancedPublicKeySecurityHandler();
        private TElPDFDocument m_CurrDoc;
        private string credentialId;
        private string accessToken;
        private string pin;
        private string otp;
        private string filePath;
        private string outFile;
        private string m_CurrOrigFile = "";
        private string m_CurrTempFile = "";
        private FileStream m_CurrStream = null;
        TElMemoryCertStorage m_CertStorage = new TElMemoryCertStorage();
        ILog logger = LogManager.GetLogger("DebugLogger");
        public BlackBoxSignature(string encodedCertificate,string filepath,string outFile, string accessToken, string credentialId, string pin, string otp)
        {
            SBUtils.Unit.SetLicenseKey("licenseKeyHere");

            byte[] data = Convert.FromBase64String(encodedCertificate);

            SBX509.TElX509Certificate cert = new SBX509.TElX509Certificate();
            cert.LoadFromBufferAuto(data, 0, data.Length, "");

            cert.ToString();


            this.filePath = filepath;
            this.accessToken = accessToken;
            this.credentialId = credentialId;
            this.pin = pin;
            this.otp = otp;
            this.outFile = outFile;
          
            m_CertStorage.Add(cert, false);
            m_Handler.CertStorage = m_CertStorage;
        }
        private bool CloseCurrentDocument(bool saveChanges)
        {
            bool Res = true;
            try
            {
                if (m_CurrDoc != null)
                {
                    try
                    {
                        m_CurrDoc.Close(saveChanges);
                    }
                    finally
                    {
                        m_CurrDoc = null;
                    }
                }
             }
            catch (Exception ex)
            {
                Res = false;

            }
            if (m_CurrStream != null)
            {
                DeleteTemporaryFile(saveChanges);
            }

            return Res;
        }
        private void PrepareTemporaryFile(string srcFile)
        {
            string TempPath = Path.GetTempFileName();
            System.IO.File.Copy(srcFile, TempPath, true);
            m_CurrStream = new FileStream(TempPath, FileMode.Open, FileAccess.ReadWrite);
            m_CurrOrigFile = this.outFile;
            m_CurrTempFile = TempPath;
        }

        private void DeleteTemporaryFile(bool saveChanges)
        {
            if (m_CurrStream != null)
            {
                m_CurrStream.Close();
                m_CurrStream = null;
            }
            if (saveChanges)
            {
                System.IO.File.Copy(m_CurrTempFile, m_CurrOrigFile, true);
            }
            File.Delete(m_CurrTempFile);
            m_CurrTempFile = "";
            m_CurrOrigFile = "";
        }

        public void SignPDF()
        {
            SBPDF.Unit.Initialize();
            SBPAdES.Unit.Initialize();
            SBPDFSecurity.Unit.Initialize();

            SBHTTPCRL.Unit.RegisterHTTPCRLRetrieverFactory();
            SBHTTPOCSPClient.Unit.RegisterHTTPOCSPClientFactory();
            SBHTTPCertRetriever.Unit.RegisterHTTPCertificateRetrieverFactory();

            SBPDF.Unit.UnregisterSecurityHandler(SBPDFSecurity.TElPDFPublicKeySecurityHandler.MetaClass.Instance);
            SBPDF.Unit.RegisterSecurityHandler(SBPDFSecurity.TElPDFPublicKeySecurityHandler.MetaClass.Instance);


            PrepareTemporaryFile(this.filePath);
            try
            {
                m_CurrDoc = new TElPDFDocument();
                try
                {
                    m_CurrDoc.OwnActivatedSecurityHandlers = true;
                    m_CurrDoc.Open(m_CurrStream);
                }
                catch (Exception)
                {
                    m_CurrDoc = null;
                    throw;
                }
            }
            catch (Exception)
            {
                DeleteTemporaryFile(false);
                throw;
            }

            int idx = m_CurrDoc.AddSignature();
            
            TElPDFSignature sig = m_CurrDoc.get_Signatures(idx);
            
            sig.Handler = m_Handler;
            m_Handler.PAdESSignatureType = TSBPAdESSignatureType.pastBasic;
            m_Handler.HashAlgorithm = SBConstants.Unit.SB_ALGORITHM_DGST_SHA256;
            m_Handler.OnCertValidatorPrepared -= new TSBPDFCertValidatorPreparedEvent(handler_OnCertValidatorPrepared); 

            m_Handler.OnCertValidatorPrepared += new TSBPDFCertValidatorPreparedEvent(handler_OnCertValidatorPrepared); 

            m_Handler.CustomName = "Adobe.PPKLite";

            m_Handler.RemoteSigningMode = true;
            m_Handler.OnRemoteSign += m_Handler_OnRemoteSign;
            m_Handler.IgnoreChainValidationErrors = true;


            try
            {
                TElHTTPSClient httpClient=new TElHTTPSClient() ;

                    TElHTTPTSPClient tspClient = new TElHTTPTSPClient();
                   
                        tspClient.HTTPClient = httpClient;
                        tspClient.URL = "http://timestamp.globalsign.com/scripts/timestamp.dll";
                        m_Handler.TSPClient = tspClient;

                  
            }
          catch (Exception e)
            {
                logger.Error(e.Message);
            }

            sig.SigningTime = DateTime.UtcNow;

            CloseCurrentDocument(true);


        }
        void handler_OnCertValidatorPrepared(object Sender, ref SBCertValidator.TElX509CertificateValidator CertValidator, TElX509Certificate Cert)
        {
            CertValidator.MandatoryCRLCheck = false;
            CertValidator.MandatoryOCSPCheck = false;
            CertValidator.MandatoryRevocationCheck = true;
            CertValidator.IgnoreCAKeyUsage = true;
        }
        private void m_Handler_OnRemoteSign(object Sender, byte[] Hash, ref byte[] SignedHash)
        {

            try
            {
                string base64Hash = Convert.ToBase64String(Hash);
                string SAD = cscClient.Credentials_Authorize(this.accessToken, this.credentialId, base64Hash, this.pin, this.otp);
                string signedHashString = cscClient.Sign_Hash(this.accessToken, this.credentialId, SAD, base64Hash);
                SignedHash = System.Convert.FromBase64String(signedHashString);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }


        }
    }
}
