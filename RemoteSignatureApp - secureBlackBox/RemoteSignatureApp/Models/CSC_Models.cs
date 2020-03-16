
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RemoteSignatureApp.Models
{


    //"{\"access_token\":\"ea1cb389-67bb-49e3-b070-846f6c9edb18\",\"refresh_token\":null,\"token_type\":\"Bearer\",\"expires_in\":300}"
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

    //{"credentialIds":["c6HZBVfUdpixiUtctvFvK7zYmfKZ7opkZnaZLivSIIE=","oM/ewU/724DYORP3gBeVQtzcJtxWjy9gxqSk4IsxvaA=","bIBGN233h/Tbaz2dP2XNIDgGNpOUK+g19tE+RGQFogE=","x6t8Fs3dC3qTPB+OAbYNBTVzNO+F35wJapNLSTvxZhU=","vuiHI6eBkhZSj+hXGio9N7ZOtsaNk3nZFC7jhRKbuJg=","GnGrQB/KhBs5RNewTcTTm1YNVPv4mTDJXDPmkC8wg84=","X8zLU2XYRRCLRFupGXfCokSZ6rccfQTNmu4IHOYRll8=","MsTBQweU/Sw/emLLqvR7CGxUxrh+iwc1II8XO4KGslY=","CZKtbYFvJwVqoeW+G0z7cI9YCtMwZSwiOr9c7HUNMR0=","7FaFyb3R9l4HkdT0PQAQO829FqsVTth6ZhieF+pCL1Q=","Q14vH5tDp3umY6U7JMRIFGFq97Az7YymNPx8dNRq5BE="],"nextPageToken":null}
    // "{\"nextPageToken\":null,\"credentialIDs\":[\"M+T1eIuPXQ7as3eLMUYfkFbjyMLNX1yh8vFYthUsVkU=\",\"hgSf/P5eMjc4jyhOYpGELwGZ0l7T0Mb32Fwsfb2ruvQ=\",\"Wqv3tTItg2sVLFY6MCHeI4QCMDQDGqbYXxE5jzaZ7AI=\",\"jmlxn7xK8RzPGbAFj84jy/UxOkSmrU+ceL72vbsaVpo=\",\"nvFCGzOTTJL/jHxA1IMTTCBxB0EOA0g9geCD8uwIhU8=\",\"sOnCrKeISVGsDT12wIclCldXuU/FIgYfC0uiwhxA6iI=\",\"yjbJmU+F0X/ASLy0WlW7vVonuzAFPkjf4dz9XwXIu4o=\",\"DNTELt2iIzMOk96qpDc702SH16DCovmDLSq9kPFRX5g=\",\"24Svyd6zoEBYrnaZ2QT5hmkbVzR0/vcHMDOKM3mN7bU=\",\"iEYhOl1N4qPKEHbwj+TUVSmoj+mH06Si4eksP4JMO3E=\",\"EVuOjSgct3/0itH+jFnveg8jLRtGTyHR5lwlxi0Y/eU=\",\"ezpaGqimddYcwi2nUKy6KRVM8QL+s3efe8Z7BfSn9V4=\",\"UCQ5arpPituov1avMJsUVqHW9e2dK7BxboqaOAZCCds=\",\"kppFafZ6a/aEyzgsswZsTzMCBOjZFf45jHwEmVMOcSQ=\",\"3GZUaLjJWUUMJr8JU56uZnKLc0gPLCwxFfDvifF3DPg=\",\"VDEqX0yTmKNIROIE1pwskCOw5I9YDiNogn0aMfye0qw=\",\"pSqglPnE5ICA0dlrJPBBLek2wSOSbhqFUB0aeARuXl0=\",\"TYe1+JJZkvIlteiIKlVEphz4436DOrlpDhVeQ+eW1Qo=\",\"b6Sx/insb/t8U+3xknss2OvzQ4MwBf7IbR0LpIts/ss=\",\"HVQ8PJHe+CY+lURs5QxKqZMUYXjBIXVJOQkMkzIntNM=\",\"yL4Oqi8KtD0C95ZeOif4Cm2F3LclmigNBRDxX7jFCA0=\",\"E4V6xzHpdCXvivdAfpCRXxzaBtQtjF0os45Ih3y4Sok=\",\"l4r0kZ6XDtWbwsTS+cONJiSmdw2RIEV9KnDzWx88fzI=\",\"95JcEfciPEKRJ4ar1IX7DSyhW/fXF2AD89YqlhZR2sc=\",\"2EeseLMJteXmyu7mqVjJ9b0tcO07DgXiNn6JgRSg9yU=\",\"xb3AHErp/MUzaDhWg4IqO3NIgaATnJwsVAdvohx2tO4=\",\"2Imoe1qibMxx0xIdXhuGysAQ8YQ6spL3/NoS3nPKvOE=\",\"/p+Jry3OamZaRj9a5J8qcQ+HfPUiNwIc4M3MZz2hKfA=\",\"vJlIYjCmmPAfXzUh0adE3dFBGzXC+xVzV0ggkVkewWE=\",\"gLwp/GfHk/Lre6dC8fQ2sqBRuX8YOv7Yai6Wo4Ftzew=\",\"uNWOph4J94xbJvFJNx+ZzKEiqKecW1m4J1CMnbTL6dU=\",\"Y2ZcFGef4U2GUNZ03jdVCIvRA/O6E8PY/bMmbVZtHtc=\"]}"
    // ----------------------------
    // credentialIDs with capital D
    [DataContract]
    public class Credentials_List_Response
    {
        [DataMember]
        public string[] credentialIDs { get; set; }
        [DataMember]
        public string nextPageToken { get; set; }
    }

    //{"expiresIn":0,"sad":"MIIB7AwoYXQuZ3YuZWdpei5ia3Uuc2VydmVyLmNyeXB0by5XcmFwcGVkRGF0YTCCAb4MFEFFUy9DQkMvUEtDUzVQYWRkaW5nBBIEEPORasnIYzUcI4D0RS8jFiYEggGQLDaHmPJWPqpwbHeHncHTkoubfEJYUKDe7TmDJgQ6HZUQy/Af08p88OCsIqlXAfI6OTlzs5axYRoL9T+J9wZ9TkHcV3rvAgiXUXe8iM1x12BP9+a5Ab0YvjItgyPSe50h1OEwb74m6SsXPACyOdI9HMh2GtKcHuQD+B/wOJtNoistfrLkQcBFT+9tpK3EJun3TTXDUmk/M4aXs0fI9lVQaPkyZO+lN0rEHrLnySmbwNt/bM44AFSNDLplLjPIFVwjlILsiZOtLwPW7YoUZDvoB15V9CmR1X+a+UY/Jbpr+Ztp8JZIYGwIUSF1iv/fhJDmQRzAUYHduwH5trsu7Xsx1ZNkHHMYLtTT9u9KjisZSP23PuR7M1dqHK9eKMlFPXYknkrVVOdEbLL/wE4KmSlI8UujcAeP01OT/IqmHyX4wYkf1ysTNVPMHTcX0tsckb835m5WZIJD5Pafzp1p0n3IkTgBXFNwfEU8QKCANEsg2Xtn3SwHnuRexn4uoIVg2sOg1O2cR9nIovamPuZI6EFTbg=="}
    // "{\"expiresIn\":3599,\"SAD\":\"MIIBCgwoYXQuZ3YuZWdpei5ia3Uuc2VydmVyLmNyeXB0by5XcmFwcGVkRGF0YTCB3QwUQUVTL0NCQy9QS0NTNVBhZGRpbmcEEgQQ1Vjef7AAFBT2hclA4bXEvASBsMIAae6rsGSHAO8fjRkqAuibH1LZ5H6tNy7taJ24FFkaxHJMhk63IqhZ5YhZt04mgFKuZcs1CEwM87+2PDmn0UCZjxKaPl/WbewTkPHaI3HYNRKsu6JN76YlaTc3XhA9OKuY3VcwfXgBpshfUkXSzfmDPyYwkM9/Z02h4ga/+mPlY4Lkly5RdOv3+Q5qM+vsL+y9avcc34gMYdhH/VaLCjultz+iwBB/xuKWBrCUR7kt\"}"
    // ----------------------------
    // SAD with capital letters
    [DataContract]
    public class Credentials_Authorize_Response
    {
        [DataMember]
        public int expiresIn { get; set; }
        [DataMember]
        public string SAD { get; set; }
    }

    //{"description":"Card alias: mihai2","key":{"keyStatus":"enabled","algo":["1.2.840.113549.1.1.1","1.3.14.3.2.29","1.2.840.113549.1.1.11"],"len":2048,"curve":null},"cert":{"status":"valid","certificates":["MIIFqDCCBJCgAwIBAgIICpxwGvJJXWEwDQYJKoZIhvcNAQELBQAwdDELMAkGA1UE\r\nBhMCUk8xFzAVBgNVBAoTDlRyYW5zIFNwZWQgU1JMMR8wHQYDVQQLExZGT1IgVEVT\r\nVCBQVVJQT1NFUyBPTkxZMSswKQYDVQQDEyJUcmFucyBTcGVkIE1vYmlsZSBlSURB\r\nUyBRQ0EgLSBURVNUMB4XDTE3MDMyODE1NDUzM1oXDTE4MDMyODE1NDUzM1owgY8x\r\nCzAJBgNVBAYTAlJPMQ0wCwYDVQQEDARQdXJhMRIwEAYDVQQqDAlNaWhhaUxpY2Ex\r\nRDBCBgNVBAUTOzIwMDQxMjIzNFBNMzc1MTc5OTc1MzU5NDEzMTk5NTUwNjg4MjA4\r\nMjkyNTA0MjgxODQ1MjExNjE4OTkxMRcwFQYDVQQDDA5NaWhhaUxpY2EgUHVyYTCC\r\nASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAOAmsXhgUJ+rc6d474S/3L/z\r\nomyQBTMV313ku95kDVAuQsYIXAh4DXkKgsf9xCHOq7ryTh3mm8immLN7GnBBhEDm\r\nWbWcl9vsBoVOQEl1XPr/4lX7izbHn99D2JSs/NBCWfsAKxxSaSo74pDwuOcCmhVO\r\nqNrimaJSF45sAxGxCREnc06DmWF+Dm3VLzt1EeMG5z/cD396GMAoH0nbAt+w4AtU\r\n5ip9QiYnETNQQNmNeJqeMiIK3T8n0jvqcgi/TVV6N9LuPONxjyYqOUfDNhcySA8S\r\nLcmClKDv9NgL4fu/iZJ9ecE5BV6ty61REDTWCxzgjV7+BJWzOnrL8nsEn1i/rbcC\r\nAwEAAaOCAiAwggIcMIGEBggrBgEFBQcBAQR4MHYwSAYIKwYBBQUHMAKGPGh0dHA6\r\nLy93d3cudHJhbnNzcGVkLnJvL2NhY2VydHMvdHNfbW9iaWxlX2VpZGFzX3FjYV90\r\nZXN0LmNydDAqBggrBgEFBQcwAYYeaHR0cDovL29jc3AtdGVzdC50cmFuc3NwZWQu\r\ncm8vMB0GA1UdDgQWBBSdJ1ORqiha8G/8n6unurW1jqJZlDAMBgNVHRMBAf8EAjAA\r\nMB8GA1UdIwQYMBaAFArxgEfkxFGB1CqMxpVt6YtztQTMMFAGCCsGAQUFBwEDBEQw\r\nQjAIBgYEAI5GAQEwCAYGBACORgEEMCwGBgQAjkYBBQwiaHR0cDovL3d3dy50cmFu\r\nc3NwZWQucm8vcmVwb3NpdG9yeTBVBgNVHSAETjBMMAkGBwQAi+xAAQIwPwYLKwYB\r\nBAGCuB0EAQEwMDAuBggrBgEFBQcCARYiaHR0cDovL3d3dy50cmFuc3NwZWQucm8v\r\ncmVwb3NpdG9yeTBJBgNVHR8EQjBAMD6gPKA6hjhodHRwOi8vd3d3LnRyYW5zc3Bl\r\nZC5yby9jcmwvdHNfbW9iaWxlX2VpZGFzX3FjYV90ZXN0LmNybDAOBgNVHQ8BAf8E\r\nBAMCBsAwHQYDVR0lBBYwFAYIKwYBBQUHAwIGCCsGAQUFBwMEMCIGA1UdEQQbMBmB\r\nF21paGFpLnB1cmFAdHJhbnNzcGVkLnJvMA0GCSqGSIb3DQEBCwUAA4IBAQBGEW3i\r\nuQ7VHjhnI+U3oOLfnc/IWEgmd5OXXUXqZvlM9p1Fr2YD0WDzXH2RJrmIg3hI+bnz\r\nse7tkmabbuv3EW6n4mY4Q9QsOrtvSuLJwWf7/kil6F4T54vWanYJvjPfcA4TkbRb\r\n+LInF516yn3KzWuj+JPIWEMIuuPKuJqLAE07K69PVjBuap3/jyj4g4UZSr7knHbN\r\nesMxXQx/Imzhyejfzv1hi+iE++K6jLdX8egGFX1E/1EXb8ZgA4sKgUM/zEXZnXWL\r\nP35kALrLDyPV+64/NzKG4hv6xtlrxBfc/ssKyVfFC4aEhc7Vuc6BkmSRF+4jJq2o\r\n7Jej0A1O4DK0yS+B"],"issuerDN":"CN=Trans Sped Mobile eIDAS QCA - TEST,OU=FOR TEST PURPOSES ONLY,O=Trans Sped SRL,C=RO","serialNumber":"a9c701af2495d61","subjectDN":"CN=MihaiLica Pura,serialNumber=200412234PM375179975359413199550688208292504281845211618991,givenName=MihaiLica,SN=Pura,C=RO","validFrom":"Tue Mar 28 18:45:33 EEST 2017","validTo":"Wed Mar 28 18:45:33 EEST 2018"},"authmode":"explicit","scal":"1","multisign":false,"lang":null,"otp":{"type":"online","presence":"true","format":"alphanumeric","label":"TAN:","description":null,"provider":null,"id":null},"pin":{"presence":"true","format":"alphanumeric","label":"PIN:","description":"some pin description"}}
    //"{\"description\":\"Card alias: mihai6\",\"key\":{\"status\":\"enabled\",\"algo\":[\"1.2.840.113549.1.1.1\",\"1.3.14.3.2.29\",\"1.2.840.113549.1.1.11\",\"1.2.840.113549.1.1.13\"],\"len\":2048,\"curve\":null},\"cert\":{\"status\":\"valid\",\"certificates\":[\"MIIFozCCBIugAwIBAgIIez1ERPzn4a8wDQYJKoZIhvcNAQELBQAwdDELMAkGA1UE\\r\\nBhMCUk8xFzAVBgNVBAoTDlRyYW5zIFNwZWQgU1JMMR8wHQYDVQQLExZGT1IgVEVT\\r\\nVCBQVVJQT1NFUyBPTkxZMSswKQYDVQQDEyJUcmFucyBTcGVkIE1vYmlsZSBlSURB\\r\\nUyBRQ0EgLSBURVNUMB4XDTE3MTEzMDIwMzM0NVoXDTE4MTEzMDIwMzM0NVowgYox\\r\\nCzAJBgNVBAYTAlJPMQ0wCwYDVQQEEwRQdXJhMRMwEQYDVQQqEwpNaWhhaS1MaWNh\\r\\nMT0wOwYDVQQFEzQyMDA0MTIyMzRQTTA3Rjc5MzkwNTJEOEMyMEIyRTVGOTVDMzVD\\r\\nQUFBOTBGM0QyNzM4MUVDMRgwFgYDVQQDEw9NaWhhaS1MaWNhIFB1cmEwggEiMA0G\\r\\nCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCl2s/qUR6jSjMLJAxz6kEt9vZwj8/Y\\r\\nJtHOIm6TTzMQ1pc+CIeYC4PzHtQ1EyXKbqWa7tI0io0kBl33MmO3O1veKVptbIae\\r\\nCgU6+7TKl1Ku5f8d0xUXLqllzJww7EITmK0ExTNj/t4IL038UyFy9JdUrmQ9RX/D\\r\\nmMWmXzE8660bdcRR1DLYngJug8bTm7DSip9rZXEifJzKwU6Y+5h99z4VBCy48crv\\r\\n43RQ47yaigcLtlqU70qgsLGC4ciEuC5uI/jCL7xDA28DVUTRSRYdSaH2LoR1t2w7\\r\\nboB9spfv/aW+OYKF8bveX9oHIo4sFdXKVYSc3+Ha3abyaQSNgSKJTR8fAgMBAAGj\\r\\nggIgMIICHDCBhAYIKwYBBQUHAQEEeDB2MEgGCCsGAQUFBzAChjxodHRwOi8vd3d3\\r\\nLnRyYW5zc3BlZC5yby9jYWNlcnRzL3RzX21vYmlsZV9laWRhc19xY2FfdGVzdC5j\\r\\ncnQwKgYIKwYBBQUHMAGGHmh0dHA6Ly9vY3NwLXRlc3QudHJhbnNzcGVkLnJvLzAd\\r\\nBgNVHQ4EFgQU1JvU2h3y6BlZMmQ37WhG/1Tv0dowDAYDVR0TAQH/BAIwADAfBgNV\\r\\nHSMEGDAWgBQK8YBH5MRRgdQqjMaVbemLc7UEzDBQBggrBgEFBQcBAwREMEIwCAYG\\r\\nBACORgEBMAgGBgQAjkYBBDAsBgYEAI5GAQUMImh0dHA6Ly93d3cudHJhbnNzcGVk\\r\\nLnJvL3JlcG9zaXRvcnkwVQYDVR0gBE4wTDAJBgcEAIvsQAECMD8GCysGAQQBgrgd\\r\\nBAEBMDAwLgYIKwYBBQUHAgEWImh0dHA6Ly93d3cudHJhbnNzcGVkLnJvL3JlcG9z\\r\\naXRvcnkwSQYDVR0fBEIwQDA+oDygOoY4aHR0cDovL3d3dy50cmFuc3NwZWQucm8v\\r\\nY3JsL3RzX21vYmlsZV9laWRhc19xY2FfdGVzdC5jcmwwDgYDVR0PAQH/BAQDAgbA\\r\\nMB0GA1UdJQQWMBQGCCsGAQUFBwMCBggrBgEFBQcDBDAiBgNVHREEGzAZgRdtaWhh\\r\\naS5wdXJhQHRyYW5zc3BlZC5ybzANBgkqhkiG9w0BAQsFAAOCAQEAJbDqpKM7odps\\r\\n1TCyaoYbL6SYbgTHt8r/v80c/x04x40Wemehnx+gKWrNefXfBLhkxkIY3JytQu9s\\r\\nvfKgETDGXQaTm7fpisVv3sFt5YYL5BOa6ytzIkrN64kc9Lh7IFblsWFzfC1gOArP\\r\\nNJQIGd1Qc5zDHqMh19Tq3G+XqxwitXKg3egg7J0tG+S2AXqHnRSiVRc7WqObvtm5\\r\\n18BcWbvjv0X9z8nh4fZfM12eiumrchazY8oaayZETlSwdOgY0n9mbbCJR/9lIM23\\r\\nj74eSsG/OfBld7AxbJALc8tkcY+SlquPI8WkiLdB/R5ECoJMd00ZqR7qPNZdwYaS\\r\\nFJmE0plJlA==\"],\"issuerDN\":\"CN=Trans Sped Mobile eIDAS QCA - TEST,OU=FOR TEST PURPOSES ONLY,O=Trans Sped SRL,C=RO\",\"serialNumber\":\"7b3d4444fce7e1af\",\"subjectDN\":\"CN=Mihai-Lica Pura,serialNumber=200412234PM07F7939052D8C20B2E5F95C35CAAA90F3D27381EC,givenName=Mihai-Lica,SN=Pura,C=RO\",\"validFrom\":\"20171130203345Z\",\"validTo\":\"20181130203345Z\"},\"multisign\":false,\"lang\":\"ro-RO\",\"authMode\":\"explicit\",\"SCAL\":\"2\",\"PIN\":{\"presence\":\"true\",\"format\":\"A\",\"label\":\"Sms Tan\",\"description\":\"Signatur Pin\"},\"OTP\":{\"type\":\"online\",\"presence\":\"true\",\"format\":\"A\",\"label\":\"Sms Tan\",\"description\":\"Current Otp\",\"provider\":null,\"ID\":\"\"}}"
    // ----------------------------
    // status instead of keyStatus
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
    public class Timestamp
    {
        [DataMember]
        public string timestamp { get; set; }
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

    //"otp":{"type":"online","presence":"true","format":"alphanumeric","label":"TAN:","description":null,"provider":null,"id":null}
    [DataContract]
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

    //"pin":{"presence":"true","format":"alphanumeric","label":"PIN:","description":"some pin description"}
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

    //"{\"signatures\":[\"lzn0q90IHIenHyHFfQN4GAS/G+FAKaj9BhYSJVRyMLAVEQZGyhstUP/iNtR+w1Kz\\r\\nmaGqYfeNa3wrv06YF6vKqcoC65d/wwBSWPhakKlFSZqFENDZvDDAF5Ab7sDTcSzt\\r\\nh4UISovVM3vcTyA8uHnbIOB2jlu14n47gEiE+2YLgTnS0m2i69CnaSTGzgG6B1GV\\r\\nTJa8+xQ/ZVKncxpWAgsojOeJ8I6aHT236f3nSXnTwp7TAxUwifv4YiklU46fdIll\\r\\nXSlnAHfsaw360/SsvBlx6TjI4JdWtMdzqDuQXmKa8YR9JCWxFq4MyOzD69dACCZl\\r\\nrD+RjSMSNMb841DltpZyuA==\"]}"
    [DataContract]
    public class Signatures_SignHash_Response
    {
        [DataMember]
        public string[] signatures { get; set; }
    }

    public class CredentialViewModel : IEnumerable<CredentialViewModel>
    {
        public CredentialViewModel()
        {
        }

        public CredentialViewModel(string id, string alias, string status)
        {
            Id = id;
            Alias = alias;
            Status = status;
        }
        public string Id { get; set; }
        public string Alias { get; set; }
        public string Status { get; set; }

        public IEnumerator<CredentialViewModel> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }

}