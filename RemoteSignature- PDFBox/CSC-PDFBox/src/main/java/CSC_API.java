import JSON_FORMATS.*;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.itextpdf.io.codec.Base64;
import java.net.URL;
import java.io.*;
import java.net.HttpURLConnection;

public class CSC_API {

    private static String BaseURL = "https://msign-test.transsped.ro/csc/v0/";

    public static String OAuth2_Token(String code) {
        try {
            String methodURL = BaseURL + "oauth2/token";
            URL myURL = new URL(methodURL);
            HttpURLConnection myURLConnection = (HttpURLConnection) myURL.openConnection();

          
            String postData ="{ \"grant_type\": \"authorization_code\", \"code\": \"" + code + "\", \"client_id\": \"client_ID\", \"client_secret\": \"client_Secret\", \"redirect_uri\": \"https://localhost:44300/\"}";
            
         
            myURLConnection.setRequestMethod("POST");
            myURLConnection.setRequestProperty("Content-Type", "application/json");
            myURLConnection.setRequestProperty("Content-Length", "" + postData.getBytes().length);
            myURLConnection.setRequestProperty("Content-Language", "en-US");
            myURLConnection.setUseCaches(false);
            myURLConnection.setDoInput(true);
            myURLConnection.setDoOutput(true);


            DataOutputStream wr = new DataOutputStream(
                    myURLConnection.getOutputStream());
            wr.writeBytes(postData);
            wr.close();

            InputStream is = myURLConnection.getInputStream();
            BufferedReader rd = new BufferedReader(new InputStreamReader(is));
            StringBuilder response = new StringBuilder(); // or StringBuffer if Java version 5+
            String line;
            while ((line = rd.readLine()) != null) {
                response.append(line);
                response.append('\r');
            }
            rd.close();

            OAuth2TokenResponse Resp = new ObjectMapper().readValue(response.toString(), OAuth2TokenResponse.class);
            return Resp.AccessToken;

        } catch (Exception e) {
            e.printStackTrace();
        } finally {

        }

        return null;
    }

    public static String[] CredentialsList(String AccessToken) {
        try {
            String methodURL = BaseURL + "credentials/list";
            URL myURL = new URL(methodURL);
            HttpURLConnection myURLConnection = (HttpURLConnection) myURL.openConnection();

            String bearerAuth = "Bearer " + AccessToken;
            String postData = "{ }";

            myURLConnection.setRequestProperty("Authorization", bearerAuth);
            myURLConnection.setRequestMethod("POST");
            myURLConnection.setRequestProperty("Content-Type", "application/json");
            myURLConnection.setRequestProperty("Content-Length", "" + postData.getBytes().length);
            myURLConnection.setRequestProperty("Content-Language", "en-US");
            myURLConnection.setUseCaches(false);
            myURLConnection.setDoInput(true);
            myURLConnection.setDoOutput(true);


            DataOutputStream wr = new DataOutputStream(
                    myURLConnection.getOutputStream());
            wr.writeBytes(postData);
            wr.close();

            InputStream is = myURLConnection.getInputStream();
            BufferedReader rd = new BufferedReader(new InputStreamReader(is));
            StringBuilder response = new StringBuilder(); // or StringBuffer if Java version 5+
            String line;
            while ((line = rd.readLine()) != null) {
                response.append(line);
                response.append('\r');
            }
            rd.close();

            CredentialsListResponse Resp = new ObjectMapper().readValue(response.toString(), CredentialsListResponse.class);
            return Resp.CredentialIds;
        } catch (Exception e) {
            e.printStackTrace();
        } finally {

        }

        return null;
    }

    public static CredentialsInfoResponse CredentialsInfo(String accessToken,String credentialId) {
        try {
            String methodURL = BaseURL + "credentials/info";
            URL myURL = new URL(methodURL);
            HttpURLConnection myURLConnection = (HttpURLConnection) myURL.openConnection();

            String bearerAuth = "Bearer " + accessToken;
            String postData = "{ \"credentialID\": \"" + credentialId + "\",\"certInfo\": \"true\"}";

            myURLConnection.setRequestProperty("Authorization", bearerAuth);
            myURLConnection.setRequestMethod("POST");
            myURLConnection.setRequestProperty("Content-Type", "application/json");
            myURLConnection.setUseCaches(false);
            myURLConnection.setDoInput(true);
            myURLConnection.setDoOutput(true);


            DataOutputStream wr = new DataOutputStream(
                    myURLConnection.getOutputStream());
            wr.writeBytes(postData);
            wr.close();

            InputStream is = myURLConnection.getInputStream();
            BufferedReader rd = new BufferedReader(new InputStreamReader(is));
            StringBuilder response = new StringBuilder(); 
            String line;
            while ((line = rd.readLine()) != null) {
                response.append(line);
                response.append('\r');
            }
            rd.close();

            CredentialsInfoResponse Resp = new ObjectMapper().readValue(response.toString(), CredentialsInfoResponse.class);
            return Resp;
        } catch (Exception e) {
            e.printStackTrace();
        } finally {

        }

        return null;
    }

    public static Boolean CredentialsSendOTP(String accessToken,String credentialId) {
        try {
            String methodURL = BaseURL + "credentials/sendOTP";
            URL myURL = new URL(methodURL);
            HttpURLConnection myURLConnection = (HttpURLConnection) myURL.openConnection();

            String bearerAuth = "Bearer " + accessToken;
            String postData = "{ \"credentialID\": \"" + credentialId + "\"}";

            myURLConnection.setRequestProperty("Authorization", bearerAuth);
            myURLConnection.setRequestMethod("POST");
            myURLConnection.setRequestProperty("Content-Type", "application/json");
            myURLConnection.setUseCaches(false);
            myURLConnection.setDoInput(true);
            myURLConnection.setDoOutput(true);


            DataOutputStream wr = new DataOutputStream(
                    myURLConnection.getOutputStream());
            wr.writeBytes(postData);
            wr.close();

            InputStream is = myURLConnection.getInputStream();
            BufferedReader rd = new BufferedReader(new InputStreamReader(is));
            StringBuilder response = new StringBuilder();
            String line;
            while ((line = rd.readLine()) != null) {
                response.append(line);
                response.append('\r');
            }
            rd.close();

            return true;

        } catch (Exception e) {
            e.printStackTrace();
            return false;
        }
        finally {

        }


    }

    public static String CredentialsAuthorize(String base64Hash, String accessToken,String credentialId, String PIN, String OTP ) {
        try {
            String methodURL = BaseURL + "credentials/authorize";
            URL myURL = new URL(methodURL);
            HttpURLConnection myURLConnection = (HttpURLConnection) myURL.openConnection();

            String bearerAuth = "Bearer " + accessToken;
            String postData = "{ \"credentialID\": \"" + credentialId + "\", \"numSignatures\": \"1\", \"hash\": [\"" + base64Hash + "\"], \"PIN\": \"" + PIN + "\", \"OTP\": \"" + OTP + "\"  }";

            myURLConnection.setRequestProperty("Authorization", bearerAuth);
            myURLConnection.setRequestMethod("POST");
            myURLConnection.setRequestProperty("Content-Type", "application/json");

            myURLConnection.setUseCaches(false);
            myURLConnection.setDoInput(true);
            myURLConnection.setDoOutput(true);


            DataOutputStream wr = new DataOutputStream(
                    myURLConnection.getOutputStream());
            wr.writeBytes(postData);
            wr.close();

            InputStream is = myURLConnection.getInputStream();
            BufferedReader rd = new BufferedReader(new InputStreamReader(is));
            StringBuilder response = new StringBuilder(); // or StringBuffer if Java version 5+
            String line;
            while ((line = rd.readLine()) != null) {
                response.append(line);
                response.append('\r');
            }
            rd.close();
            CredentialsAuthorizeResponse resp = new ObjectMapper().readValue(response.toString(), CredentialsAuthorizeResponse.class);
            return resp.SAD;
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    public static String SignatureSignHash(String base64Hash, String accessToken,String credentialId, String SAD ) {
        try {
            String methodURL = BaseURL + "signatures/signHash";
            URL myURL = new URL(methodURL);
            HttpURLConnection myURLConnection = (HttpURLConnection) myURL.openConnection();

            String bearerAuth = "Bearer " + accessToken;
            String postData = "{ \"credentialID\": \"" + credentialId + "\", \"signAlgo\": \"1.2.840.113549.1.1.11\", \"hashAlgo\": \"2.16.840.1.101.3.4.2.1\", \"SAD\": \"" + SAD + "\", \"hash\": [\"" + base64Hash + "\"]}";

            myURLConnection.setRequestProperty("Authorization", bearerAuth);
            myURLConnection.setRequestMethod("POST");
            myURLConnection.setRequestProperty("Content-Type", "application/json");

            myURLConnection.setUseCaches(false);
            myURLConnection.setDoInput(true);
            myURLConnection.setDoOutput(true);


            DataOutputStream wr = new DataOutputStream(
                    myURLConnection.getOutputStream());
            wr.writeBytes(postData);
            wr.close();

            InputStream is = myURLConnection.getInputStream();
            BufferedReader rd = new BufferedReader(new InputStreamReader(is));
            StringBuilder response = new StringBuilder(); // or StringBuffer if Java version 5+
            String line;
            while ((line = rd.readLine()) != null) {
                response.append(line);
                response.append('\r');
            }
            rd.close();


            SignaturesSignHashResponse resp = new ObjectMapper().readValue(response.toString(), SignaturesSignHashResponse.class);
            return resp.signatures[0];
        } catch (Exception e) {
            System.out.println("Eroare la calcularea efectiva a semnaturii\n");
            e.printStackTrace();
            return null;
        }
    }

    public static String SignaturesTimestamp(String base64Hash, String accessToken) {
            try {
                String methodURL = BaseURL + "signatures/timestamp";
                URL myURL = new URL(methodURL);
                HttpURLConnection myURLConnection = (HttpURLConnection) myURL.openConnection();

                String bearerAuth = "Bearer " + accessToken;
                String postData = "{  \"hash\":\"" + base64Hash + "\", \"hashAlgo\": \"2.16.840.1.101.3.4.2.1\" }";

                myURLConnection.setRequestProperty("Authorization", bearerAuth);
                myURLConnection.setRequestMethod("POST");
                myURLConnection.setRequestProperty("Content-Type", "application/json");

                myURLConnection.setUseCaches(false);
                myURLConnection.setDoInput(true);
                myURLConnection.setDoOutput(true);


                DataOutputStream wr = new DataOutputStream(
                        myURLConnection.getOutputStream());
                wr.writeBytes(postData);
                wr.close();

                InputStream is = myURLConnection.getInputStream();
                BufferedReader rd = new BufferedReader(new InputStreamReader(is));
                StringBuilder response = new StringBuilder(); 
                String line;
                while ((line = rd.readLine()) != null) {
                    response.append(line);
                    response.append('\r');
                }
                rd.close();

                SignaturesTimestampResponse resp = new ObjectMapper().readValue(response.toString(), SignaturesTimestampResponse.class);
                return resp.timestamp;
            } catch (Exception e) {
                return null;
            }
    }
}
