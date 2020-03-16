using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace CSC_Signature_Test
{
 
    [DataContract]
    public class OAuth2_Token_Response
    {
        [DataMember]
        public string access_token { get; set; }
        [DataMember]
        public string refresh_token { get; set; }
        [DataMember]
        public string token_type { get; set; }
        [DataMember]
        public int expires_in { get; set; }
    }

    [DataContract]
    public class Credentials_List_Response
    {
        [DataMember]
        public string[] credentialIDs { get; set; }
        [DataMember]
        public string nextPageToken { get; set; }
    }

   
    [DataContract]
    public class Credentials_Authorize_Response
    {
        [DataMember]
        public int expiresIn { get; set; }
        [DataMember]
        public string SAD { get; set; }
    }

    [DataContract]
    public class Credentials_Info_Response
    {
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public Key key { get; set; }
        [DataMember]
        public Certificate cert { get; set; }
        [DataMember]
        public string authmode { get; set; }
        [DataMember]
        public string scal { get; set; }
        [DataMember]
        public bool multisign { get; set; }
        [DataMember]
        public string lang { get; set; }
        [DataMember]
        public OTP otp { get; set; }
        [DataMember]
        public PIN pin { get; set; }
    }

    [DataContract]
    public class Key
    {
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string[] algo { get; set; }
        [DataMember]
        public int len { get; set; }
        [DataMember]
        public string curve { get; set; }
    }

    [DataContract]
    public class Certificate
    {
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string[] certificates { get; set; }
        [DataMember]
        public string issuerDN { get; set; }
        [DataMember]
        public string serialNumber { get; set; }
        [DataMember]
        public string subjectDN { get; set; }
        [DataMember]
        public string validFrom { get; set; }
        [DataMember]
        public string validTo { get; set; }
    }

 
    public class OTP
    {
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string presence { get; set; }
        [DataMember]
        public string format { get; set; }
        [DataMember]
        public string label { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string provider { get; set; }
        [DataMember]
        public string id { get; set; }
    }

    [DataContract]
    public class PIN
    {
        [DataMember]
        public string presence { get; set; }
        [DataMember]
        public string format { get; set; }
        [DataMember]
        public string label { get; set; }
        [DataMember]
        public string description { get; set; }
    }

   [DataContract]
    public class Signatures_SignHash_Response
    {
        [DataMember]
        public string[] signatures { get; set; }
    }

    [DataContract]
    public class Timestamp
    {
        [DataMember]
        public string timestamp { get; set; }
    }

}