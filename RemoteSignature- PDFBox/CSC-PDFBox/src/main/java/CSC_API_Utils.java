import JSON_FORMATS.CredentialsInfoResponse;
import com.itextpdf.io.codec.Base64;

import java.io.ByteArrayInputStream;
import java.io.FileInputStream;
import java.security.cert.*;

public class CSC_API_Utils {


 
    public static Certificate[] GetCertChain(String accessToken, String credentialId)
    {
        Certificate[] chain = new Certificate[] {GetSigningCertificate(accessToken,credentialId),GetCACertificate()};

        return chain;
    }



    public static Certificate GetSigningCertificate(String accessToken, String credentialId)
    {
        try {
            CredentialsInfoResponse info = CSC_API.CredentialsInfo(accessToken, credentialId);
            		//new CredentialsInfoResponse();

            byte []encodedCert = Base64.decode(info.cert.certificates[0]);
 
            ByteArrayInputStream inputStream =  new ByteArrayInputStream(encodedCert);
        
            CertificateFactory certificateFactory = CertificateFactory.getInstance("X.509");
            X509Certificate cert = (X509Certificate)certificateFactory.generateCertificate(inputStream);

            return cert;
        }catch(Exception e)
        {
            e.printStackTrace();
            return null;
        }
    }
    public static Certificate GetCACertificate()
    {
        try {
        	CertificateFactory fact = CertificateFactory.getInstance("X.509");
        	FileInputStream is = new FileInputStream ("E:\\certificate.crt");
        	X509Certificate cert = (X509Certificate) fact.generateCertificate(is);
            return cert;
        }catch(Exception e)
        {
            e.printStackTrace();
            return null;
        }
    }

}
