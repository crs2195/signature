using iText.Kernel.XMP.Impl;
using iText.Signatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSC_Signature_Test
{
    class CSCPAdESSignature : IExternalSignature
    {

        CSC_API_Client clt=new CSC_API_Client();
        private String accessToken;
        private String encryptionAlgorithm;
        private String hashAlgorithm;
        private String credentialId;
        private String pin;
        private String otp;

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
            String HashToBeSigned = ComputeHash(bytes);

            string SAD = clt.Credentials_Authorize(this.accessToken, this.credentialId, HashToBeSigned, this.pin, this.otp);

            string signature = clt.Sign_Hash( this.accessToken, this.credentialId, SAD, HashToBeSigned);

            //pin = null;
            //otp = null;
            return Encoding.Unicode.GetBytes(Base64.Decode(signature));
        }

        //public  byte[] Sign(byte[] bytes)
        //{
        //    string BASE_URL = "https://msign-test.transsped.ro/csc/v0/";
        //    var uri = BASE_URL + "credentials/authorize";
        //    // Create a request using a URL that can receive a post. 
        //    WebRequest request = WebRequest.Create(uri);
        //    // Set the Method property of the request to POST.
        //    request.Method = "POST";
        //    // Create POST data and convert it to a byte array.

        //    request.Headers.Add("Bearer", this.accessToken);
        //    request.ContentType = "application/json";


        //    String HashToBeSigned = ComputeHash(bytes);
        //    var postData = "{ \"credentialID\": \"" + this.credentialId + "\", \"numSignatures\": \"1\", \"hash\": [\"" + HashToBeSigned + "\"], \"PIN\": \"" + pin + "\", \"OTP\": \"" + otp + "\"  }";
        //    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        //    // Set the ContentType property of the WebRequest.

        //    // Set the ContentLength property of the WebRequest.
        //    request.ContentLength = byteArray.Length;
        //    // Get the request stream.
        //    Stream dataStream = request.GetRequestStream();
        //    // Write the data to the request stream.
        //    dataStream.Write(byteArray, 0, byteArray.Length);

        //    dataStream.Close();
        //    // Get the response.
        //    WebResponse response = request.GetResponse();
        //    // Display the status.
        //    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        //    // Get the stream containing content returned by the server.
        //    dataStream = response.GetResponseStream();
        //    // Open the stream using a StreamReader for easy access.
        //    StreamReader reader = new StreamReader(dataStream);
        //    // Read the content.
        //    string responseFromServer = reader.ReadToEnd();
        //    // Display the content.
        //    Console.WriteLine(responseFromServer);
        //    // Clean up the streams.
        //    reader.Close();
        //    dataStream.Close();
        //    response.Close();
        //    return Encoding.Unicode.GetBytes(Base64.Decode("dg"));

        //}

        //public byte[] Sign(byte[] bytes)
        //{
        //    string BASE_URL = "https://msign-test.transsped.ro/csc/v0/";
        //    var uri = BASE_URL + "credentials/authorize";
        //    // Create a request using a URL that can receive a post. 
        //    WebRequest request = WebRequest.Create(uri);
        //    // Set the Method property of the request to POST.
        //    request.Method = "POST";
        //    // Create POST data and convert it to a byte array.

        //    request.Headers.Add("Bearer", this.accessToken);
        //    request.ContentType = "application/json";


        //    String HashToBeSigned = ComputeHash(bytes);
        //    var postData = "{ \"credentialID\": \"" + this.credentialId + "\", \"numSignatures\": \"1\", \"hash\": [\"" + "123" + "\"], \"PIN\": \"" + pin + "\", \"OTP\": \"" + otp + "\"  }";
        //    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        //    // Set the ContentType property of the WebRequest.

        //    // Set the ContentLength property of the WebRequest.
        //    request.ContentLength = byteArray.Length;
        //    // Get the request stream.
        //    Stream dataStream = request.GetRequestStream();
        //    // Write the data to the request stream.
        //    dataStream.Write(byteArray, 0, byteArray.Length);

        //    dataStream.Close();
        //    // Get the response.
        //    WebResponse response = request.GetResponse();
        //    // Display the status.
        //    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        //    // Get the stream containing content returned by the server.
        //    dataStream = response.GetResponseStream();
        //    // Open the stream using a StreamReader for easy access.
        //    StreamReader reader = new StreamReader(dataStream);
        //    // Read the content.
        //    string responseFromServer = reader.ReadToEnd();
        //    // Display the content.
        //    Console.WriteLine(responseFromServer);
        //    // Clean up the streams.
        //    reader.Close();
        //    dataStream.Close();
        //    response.Close();
        //    return Encoding.Unicode.GetBytes(Base64.Decode("dg"));

        //}

        //public byte[] Sign(byte[] message)
        //{
        //    throw new NotImplementedException();
        //}

        private String ComputeHash(byte[] message)
        {
            try
            {
                SHA256Managed hashstring = new SHA256Managed();
                byte[] hash = hashstring.ComputeHash(message);
                //string hashBase64 = "N8SY8c2XTBzZPrDk2TKJ+Cp4bgzQQJVVV9UF1cKyGFI=";


                string hashBase64 = Convert.ToBase64String(hash);
                return hashBase64;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }


    }
}



