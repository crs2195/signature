

using iTextSharp.text.pdf.security;
using log4net;
using System;
using System.Security.Cryptography;

namespace RemoteSignatureApp.Classes
{
   
    class CSCPAdESSignature : IExternalSignature
    { ILog logger = LogManager.GetLogger("DebugLogger");

        CSC_API_Client clt=new CSC_API_Client();
        private String accessToken;
        private String encryptionAlgorithm;
        private String hashAlgorithm;
        private String credentialId;
        private String pin;
        private String otp;

        public CSCPAdESSignature()
        {
            

        }
        public CSCPAdESSignature(String AccessToken, String CredentialId, String Pin, String Otp)
        {
            this.accessToken = AccessToken;
            this.credentialId = CredentialId;
            this.encryptionAlgorithm = "RSA";
            this.hashAlgorithm = "SHA-256";
            this.pin = Pin;
            this.otp = Otp; ;


        }

        public CSCPAdESSignature(String AccessToken, String CredentialId, String Pin, String Otp, String EncryptionAlgorithm, String HashAlgorithm)
        {
            this.accessToken = AccessToken;
            this.credentialId = CredentialId;
            this.encryptionAlgorithm = EncryptionAlgorithm;
            this.hashAlgorithm = HashAlgorithm;
            this.pin = Pin;
            this.otp = Otp;
        }

        public String GetEncryptionAlgorithm()
        {
            return this.encryptionAlgorithm;
        }

     

        public String GetHashAlgorithm()
        {
            return this.hashAlgorithm;
        }

      

        public byte[] Sign(byte[] bytes)
        {
           
            try
            {


                SHA256Managed hashstring = new SHA256Managed();
                byte[] hash = hashstring.ComputeHash(bytes);
                string hashBase64 = Convert.ToBase64String(hash);


                string SAD = clt.Credentials_Authorize(this.accessToken, this.credentialId, hashBase64, this.pin, this.otp);

                string signature = clt.Sign_Hash(this.accessToken, this.credentialId, SAD, hashBase64);
                logger.Info(signature);
                return Convert.FromBase64String(signature);
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return null;
            }
        }



    }
}



