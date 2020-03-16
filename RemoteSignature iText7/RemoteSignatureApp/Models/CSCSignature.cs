using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Signatures;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.X509;
using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC_Signature_Test
{
    public class CSCSignature
    {

        public static async Task SignPdfFileAsync(String accessToken, String credentialId, String pin, String otp, String inPath, String outPath)
        {
            try
            {
                PdfReader reader = new PdfReader(inPath);
                PdfSigner signer = new PdfSigner(reader, new FileStream(outPath,FileMode.Create), false);

                PdfSignatureAppearance appearance = signer.GetSignatureAppearance()
                        .SetReason("Reason")
                        .SetLocation("Romania")
                        .SetReuseAppearance(false);
                Rectangle rect = new Rectangle(36, 648, 200, 100);
                appearance.SetPageRect(rect).SetPageNumber(1);
                signer.SetFieldName("sig");

                IExternalSignature pks = new CSCPAdESSignature(accessToken, credentialId, pin, otp);
              
              X509Certificate [] chain = await CSC_API_Utils.GetCertChainAsync(accessToken, credentialId);

               ICrlClient signingCertCrl = new CrlClientOnline(chain);

             List<ICrlClient> crlList = new List<ICrlClient>();
               crlList.Add(signingCertCrl);

              signer.SignDetached( pks, chain, crlList, null, null, 0, PdfSigner.CryptoStandard.CADES);
            }
            catch (Exception e)
            {
              
            }
        }
    }
}
