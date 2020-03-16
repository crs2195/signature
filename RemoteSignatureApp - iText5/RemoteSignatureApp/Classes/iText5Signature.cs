
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.X509;
using RemoteSignatureApp.Models;
using System;
using System.IO;

using System.Threading.Tasks;


namespace RemoteSignatureApp.Classes
{
    public class iText5Signature
    {
        static readonly CSC_API_Client clt = new CSC_API_Client();
        public static async Task SignPdfFile(String accessToken, String credentialId, String pin, String otp, String inPath, String outPath)
        {
            try
            {
                PdfReader reader = new PdfReader(inPath);
                FileStream os = new FileStream(outPath, FileMode.Create);
                PdfStamper stamper = PdfStamper.CreateSignature(reader, os, '\0');
                PdfSignatureAppearance appearance = stamper.SignatureAppearance;
                appearance.Reason = "Test semnatura digitala";
                appearance.Location = "Bucuresti, RO";
                appearance.SetVisibleSignature(new Rectangle(36, 748, 144, 780), 1, "semnatura iText5");
                IExternalSignature signature = new CSCPAdESSignature(accessToken, credentialId, pin, otp);
                Credentials_Info_Response info = await clt.Credentials_Info(accessToken, credentialId);


                byte[] bytes = Convert.FromBase64String(info.cert.certificates[0]);
                ITSAClient tsaClient = new TSAClientBouncyCastle("http://timestamp.globalsign.com/scripts/timestamp.dll");
                X509Certificate cert = new X509CertificateParser().ReadCertificate(bytes);
               
            
                X509Certificate[] chain = new X509Certificate[] { cert };
                MakeSignature.SignDetached(appearance, signature, chain, null, null, tsaClient, 0, CryptoStandard.CMS);
            }
            catch (Exception e)
            {
              
            }
        }
    }
}
