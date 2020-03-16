using iText.Forms;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Annot;
using iText.Signatures;
using log4net;
using Org.BouncyCastle.Tsp;
using Org.BouncyCastle.X509;
using RemoteSignatureApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


namespace RemoteSignatureApp.Classes
{
    public class iTextSignature
    {
        static ILog  logger = LogManager.GetLogger("DebugLogger");
        public static async Task SignPdfFile(String accessToken, String credentialId, String pin, String otp, String inPath, String outPath)
        {
            try
            {
                PdfReader reader = new PdfReader(inPath);
                PdfSigner signer = new PdfSigner(reader, new FileStream(outPath, FileMode.Create), new StampingProperties());

                PdfSignatureAppearance appearance = signer.GetSignatureAppearance()
                        .SetReason("Test semnatura digitala")
                        .SetLocation("Bucuresti, RO")
                       
                        .SetReuseAppearance(false);
                Rectangle rect = new Rectangle(300, 690, 200, 100);
                appearance.SetPageRect(rect).SetPageNumber(1);
                signer.SetFieldName("semnatura iText7");

                IExternalSignature pks = new CSCPAdESSignature(accessToken, credentialId, pin, otp);

                X509Certificate[] chain = await CSC_API_Utils.GetCertChainAsync(accessToken, credentialId);

                ICrlClient signingCertCrl = new CrlClientOnline(chain);

                List<ICrlClient> crlList = new List<ICrlClient>();
                crlList.Add(signingCertCrl);
                ITSAClient tsaClient = new TSAClientBouncyCastle("http://timestamp.globalsign.com/scripts/timestamp.dll");


                signer.SignDetached(pks, chain, crlList, null, tsaClient, 0, PdfSigner.CryptoStandard.CADES);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
        }
    

        public static (SignaturePermissions,FileDetailsModel) InspectSignature(FileDetailsModel model, PdfDocument pdfDoc, SignatureUtil signUtil, PdfAcroForm form,
           String name, SignaturePermissions perms)
        {
            IList<PdfWidgetAnnotation> widgets = form.GetField(name).GetWidgets();


         
            PdfPKCS7 pkcs7 = null;
            (pkcs7,model) = VerifySignature(model,signUtil, name);
            logger.Error("Digest algorithm: " + pkcs7.GetHashAlgorithm());
            logger.Error("Encryption algorithm: " + pkcs7.GetEncryptionAlgorithm());
            logger.Error("Filter subtype: " + pkcs7.GetFilterSubtype());

       
            X509Certificate cert = (X509Certificate)pkcs7.GetSigningCertificate();
            logger.Error("Name of the signer: "
                                  + iText.Signatures.CertificateInfo.GetSubjectFields(cert).GetField("CN"));
            model.SignerName = iText.Signatures.CertificateInfo.GetSubjectFields(cert).GetField("CN");

            if (pkcs7.GetSignName() != null)
            {
                logger.Error("Alternative name of the signer: " + pkcs7.GetSignName());
            }

           
           logger.Error("Signed on: " + pkcs7.GetSignDate().ToUniversalTime().ToString("yyyy-MM-dd"));
            model.SignatureDate = pkcs7.GetSignDate().ToUniversalTime().ToString("yyyy-MM-dd");


            logger.Error("Location: " + pkcs7.GetLocation());
            logger.Error("Reason: " + pkcs7.GetReason());

           
            PdfDictionary sigDict = signUtil.GetSignatureDictionary(name);
            PdfString contact = sigDict.GetAsString(PdfName.ContactInfo);
            if (contact != null)
            {
               logger.Error("Contact info: " + contact);
            }

         

            return (perms,model);
        }

        public static (PdfPKCS7,FileDetailsModel) VerifySignature(FileDetailsModel model, SignatureUtil signUtil, String name)
        {
            PdfPKCS7 pkcs7 = signUtil.ReadSignatureData(name);

            logger.Error("Signature covers whole document: " + signUtil.SignatureCoversWholeDocument(name));
            logger.Error("Document revision: " + signUtil.GetRevision(name) + " of "
                                  + signUtil.GetTotalRevisions());
            logger.Error("Integrity check OK? " + pkcs7.VerifySignatureIntegrityAndAuthenticity());
            model.Integrity = pkcs7.VerifySignatureIntegrityAndAuthenticity()==true?"OK":"NOT OK";
            return (pkcs7,model);
        }

        public static List<FileDetailsModel> InspectSignatures(String path)
        {

            List<FileDetailsModel> fileModelList = new List<FileDetailsModel>();
            
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(path));
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, false);
            SignaturePermissions perms = null;
            SignatureUtil signUtil = new SignatureUtil(pdfDoc);
            IList<String> names = signUtil.GetSignatureNames();

           
            foreach (String name in names)
            {
                FileDetailsModel fileModel = new FileDetailsModel();
                logger.Error("===== " + name + " =====");
                fileModel.Signature = name;
                (perms, fileModel) = InspectSignature(fileModel,pdfDoc, signUtil, form, name, perms);
                fileModelList.Add(fileModel);
            }
            return fileModelList;
        }
        public static void addLTV(String src, String dest, IOcspClient ocsp, ICrlClient crl, ITSAClient itsaClient)
        {
            PdfReader reader = new PdfReader(src);
            PdfWriter writer = new PdfWriter(dest);
            PdfDocument pdfDoc = new PdfDocument(reader, writer, new StampingProperties().UseAppendMode());
            LtvVerification v = new LtvVerification(pdfDoc);
            SignatureUtil signatureUtil = new SignatureUtil(pdfDoc);
            IList<string> names = signatureUtil.GetSignatureNames();
            String sigName = names[names.Count - 1];
            PdfPKCS7 pkcs7 = signatureUtil.ReadSignatureData(sigName);
            if (pkcs7.IsTsp())
            {
                v.AddVerification(sigName, ocsp, crl, LtvVerification.CertificateOption.WHOLE_CHAIN,
                    LtvVerification.Level.OCSP_CRL, LtvVerification.CertificateInclusion.NO);
            }
            else
            {
                foreach (var name in names)
                {
                    v.AddVerification(name, ocsp, crl, LtvVerification.CertificateOption.WHOLE_CHAIN,
                        LtvVerification.Level.OCSP_CRL, LtvVerification.CertificateInclusion.NO);
                }
            }
            v.Merge();
            pdfDoc.Close();
        }
    }
}
