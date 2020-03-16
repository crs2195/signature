using iText.Kernel.XMP.Impl;
using iText.Signatures;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using RemoteSignatureApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RemoteSignatureApp.Classes
{
    class CSC_API_Utils
    {
        static readonly CSC_API_Client clt=new CSC_API_Client();
        public static async Task<Org.BouncyCastle.X509.X509Certificate[]> GetCertChainAsync(String accessToken, String credentialId)
        {
            Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] {await GetSigningCertificateAsync(accessToken, credentialId) };

            return chain;
        }



        public static async Task<Org.BouncyCastle.X509.X509Certificate> GetSigningCertificateAsync(String accessToken, String credentialId)
        {
            try
            {
                Credentials_Info_Response info = await clt.Credentials_Info(accessToken, credentialId);


                byte[] bytes = Convert.FromBase64String(info.cert.certificates[0]);
               // var cert2 = new X509Certificate2(bytes);

                //cert.certificate[id certificatului selectat pt semnare)
                string encodedCert = Base64.Decode(info.cert.certificates[0]);

                Org.BouncyCastle.X509.X509Certificate cert = new X509CertificateParser().ReadCertificate(bytes);
                return cert;
            }
            catch (Exception e)
            {
                return null;
            }
        }
  

    }
}
