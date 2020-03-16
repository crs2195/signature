import com.itextpdf.io.codec.Base64;
import com.itextpdf.io.source.ByteArrayOutputStream;
import com.itextpdf.signatures.IExternalSignature;
import com.itextpdf.signatures.PdfPKCS7;
import com.itextpdf.kernel.pdf.PdfName;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.InputStreamReader;
import java.security.MessageDigest;

public class CSCPAdESSignature implements IExternalSignature {

    private String accessToken;
    private String encryptionAlgorithm;
    private String hashAlgorithm;
    private String credentialId;
    private String pin;
    private String otp;

    public CSCPAdESSignature(String AccessToken, String CredentialId, String Pin, String Otp)
    {
        this(AccessToken,CredentialId,Pin,Otp,"RSA","SHA-256");
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

    public String getEncryptionAlgorithm()
    {
        return this.encryptionAlgorithm;
    }

    public String getHashAlgorithm()
    {
        return this.hashAlgorithm;
    }

    public byte[] sign(byte[] bytes) throws java.security.GeneralSecurityException
    {
    	

       String HashToBeSigned = computeHash(bytes);
      

        String   SAD = CSC_API.CredentialsAuthorize(HashToBeSigned,this.accessToken,this.credentialId,this.pin,this.otp);
   
        
        String signature = CSC_API.SignatureSignHash(HashToBeSigned,this.accessToken,this.credentialId,SAD);
       
        pin = null;
        otp = null;
      
        System.gc();
      
      

        return Base64.decode(signature);
    }


    private String computeHash(byte[] message)
    {
        try {
          
            MessageDigest digest = MessageDigest.getInstance(this.getHashAlgorithm());
           
            byte[] hashBytes = digest.digest(message);
          
            String base64Hash = Base64.encodeBytes(hashBytes);
            System.out.println(base64Hash);
            return base64Hash;
        }
        catch (Exception e)
        {
            e.printStackTrace();
            return null;
        }
    }


}
