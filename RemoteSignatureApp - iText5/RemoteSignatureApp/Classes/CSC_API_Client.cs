
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using RemoteSignatureApp.Models;
using RestSharp;
using log4net;
using System;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace RemoteSignatureApp.Classes
{
    public class CSC_API_Client
    {

     
        const string BASE_URL = "https://msign-test.transsped.ro/csc/v0/";
        // info
       

        string token = "";

        public async Task OAuth2_Authorize()
        {
            var uri = BASE_URL + "info";
            using (var client = new HttpClient())
            {
                var postData = "{ }";
                var content = new StringContent(postData, Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync(uri, content))
                {
               

                    var responseJson = await response.Content.ReadAsStringAsync();
                    token = responseJson.Replace("\"", "");
                }
            }
        }


        public async Task<string> OAuth2_Token(string code)
        {
            var uri = BASE_URL + "oauth2/token";

            using (var client = new HttpClient())
            {
                

                var postData = "{ \"grant_type\": \"authorization_code\", \"code\": \"" + code + "\", \"client_id\": \"client_ID\", \"client_secret\": \"client_Secret\", \"redirect_uri\": \"https://localhost:44300/\"}";
                var content = new StringContent(postData, Encoding.UTF8, "application/json");

                using (var response = await client.PostAsync(uri, content))
                {
                    //response.EnsureSuccessStatusCode();

                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(OAuth2_Token_Response));

                    

                    Task<string> getContent = ReadResponseContentAsByteArray(response);
                    string contentResponse = await getContent;

                    System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.Unicode.GetBytes(contentResponse));
                    var objResponse = jsonSerializer.ReadObject(ms) as OAuth2_Token_Response;
                    ms.Dispose();
                    ms = null;

                    return objResponse.access_token;
                  
                }
            }
        }


        public async Task<List<string>> Credentials_List(string access_token)
        {
            var uri = BASE_URL + "credentials/list";

            using (var client = new HttpClient())
            {
              
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

              
                var postData = "{ }";
                var content = new StringContent(postData, Encoding.UTF8, "application/json");

                using (var response = await client.PostAsync(uri, content))
                {
                  
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Credentials_List_Response));

                    
                    Task<string> getContent = ReadResponseContentAsByteArray(response);
                    string contentResponse = await getContent;

                    System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.Unicode.GetBytes(contentResponse));
                    var objResponse = jsonSerializer.ReadObject(ms) as Credentials_List_Response;
                    ms.Dispose();
                    ms = null;
                    List<string> credentialIds = new List<string>();
                   foreach(var cred in objResponse.credentialIDs)
                    {
                        credentialIds.Add(cred);
                    }
                    return credentialIds;
                  
                }
            }
        }


        public async Task<Credentials_Info_Response> Credentials_Info(string access_token, string credentialId)
        {
            var uri = BASE_URL + "credentials/info";

            using (var client = new HttpClient())
            {
            
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                var postData = "{ \"credentialID\": \"" + credentialId + "\",\"certInfo\": \"true\"}";
                var content = new StringContent(postData, Encoding.UTF8, "application/json");

                using (var response = await client.PostAsync(uri, content))
                {
                    

                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Credentials_Info_Response));

                

                    Task<string> getContent = ReadResponseContentAsByteArray(response);
                    string contentResponse = await getContent;

                    System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.Unicode.GetBytes(contentResponse));
                    var objResponse = jsonSerializer.ReadObject(ms) as Credentials_Info_Response;
                    ms.Dispose();
                    ms = null;

                    return objResponse;
               
                }
            }
        }


        public async Task Credentials_SendOTP(string access_token, string credentialId)
        {
            var uri = BASE_URL + "credentials/sendOTP";

            using (var client = new HttpClient())
            {
               
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                var postData = "{ \"credentialID\": \"" + credentialId + "\"}";
                var content = new StringContent(postData, Encoding.UTF8, "application/json");

                using (var response = await client.PostAsync(uri, content))
                {

                    var responseJson = await response.Content.ReadAsStringAsync();
                    string token = responseJson.Replace("\"", "");
                }
            }
        }
        public string Credentials_Authorize(string access_token, string credentialId, string hash, string pin, string otp)
        {

            var uri = BASE_URL + "credentials/authorize";
            WebRequest request = WebRequest.Create(uri);
            request.Method = "POST";
            request.PreAuthenticate = true;
            request.Headers.Add("Authorization", "Bearer " + access_token);

            request.ContentType = "application/json";


            var postData = "{ \"credentialID\": \"" + credentialId + "\", \"numSignatures\": \"1\", \"hash\": [\"" + hash + "\"], \"PIN\": \"" + pin + "\", \"OTP\": \"" + otp + "\"  }";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);

            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Credentials_Authorize_Response));

            System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.Unicode.GetBytes(responseFromServer));
            var objResponse = jsonSerializer.ReadObject(ms) as Credentials_Authorize_Response;
            ms.Dispose();
            ms = null;
            reader.Close();
            dataStream.Close();
            response.Close();
            return objResponse.SAD;
        }


       
        public string Sign_Hash(string access_token, string credentialId, string sad, string hash)
        {
            var uri = BASE_URL + "signatures/signHash";
            WebRequest request = WebRequest.Create(uri);
            request.Method = "POST";
            request.PreAuthenticate = true;
            request.Headers.Add("Authorization", "Bearer " + access_token);

            request.ContentType = "application/json";


            var postData = "{ \"credentialID\": \"" + credentialId + "\", \"signAlgo\": \"1.2.840.113549.1.1.11\", \"hashAlgo\": \"2.16.840.1.101.3.4.2.1\", \"signAlgoParams\": \"\", \"SAD\": \"" + sad + "\", \"hash\": [\"" + hash + "\"]}";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);

            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Signatures_SignHash_Response));
            System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.Unicode.GetBytes(responseFromServer));
            var objResponse = jsonSerializer.ReadObject(ms) as Signatures_SignHash_Response;
            ms.Dispose();
            ms = null;
            reader.Close();
            dataStream.Close();
            response.Close();
            return objResponse.signatures[0];
        }
        public static string Timestamp(string access_token,  string hash)
        {
            var uri = BASE_URL + "signatures/timestamp";
            WebRequest request = WebRequest.Create(uri);
            request.Method = "POST";
            request.PreAuthenticate = true;
            request.Headers.Add("Authorization", "Bearer " + access_token);

            request.ContentType = "application/json";


            var postData = "{ \"hashAlgo\": \"2.16.840.1.101.3.4.2.1\",  \"hash\": [\"" + hash + "\"]}";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);

            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Timestamp));
            System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.Unicode.GetBytes(responseFromServer));
            var objResponse = jsonSerializer.ReadObject(ms) as Timestamp;
            ms.Dispose();
            ms = null;
            reader.Close();
            dataStream.Close();
            response.Close();
            return objResponse.timestamp;
        }

        private async Task<string> ReadResponseContentAsByteArray(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }
    }
}



