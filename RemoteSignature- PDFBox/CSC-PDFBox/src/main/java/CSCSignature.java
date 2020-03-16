


import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.security.GeneralSecurityException;
import java.security.KeyStore;
import java.security.KeyStoreException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.security.UnrecoverableKeyException;
import java.security.cert.Certificate;
import java.security.cert.CertificateException;
import java.security.cert.CertificateFactory;
import java.security.cert.X509Certificate;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Calendar;
import java.util.Collection;
import java.util.List;

import org.apache.pdfbox.cos.COSArray;
import org.apache.pdfbox.cos.COSDictionary;
import org.apache.pdfbox.cos.COSName;
import org.apache.pdfbox.cos.COSString;
import org.apache.pdfbox.examples.signature.CreateSignatureBase;

import org.apache.pdfbox.pdmodel.PDDocument;
import org.apache.pdfbox.pdmodel.interactive.digitalsignature.ExternalSigningSupport;
import org.apache.pdfbox.pdmodel.interactive.digitalsignature.PDSignature;
import org.apache.pdfbox.pdmodel.interactive.digitalsignature.SignatureOptions;

import org.bouncycastle.asn1.ASN1Primitive;

import org.bouncycastle.asn1.x509.AlgorithmIdentifier;

import org.bouncycastle.cert.X509CertificateHolder;
import org.bouncycastle.cert.jcajce.JcaCertStore;

import org.bouncycastle.cms.CMSException;

import org.bouncycastle.cms.CMSSignedData;
import org.bouncycastle.cms.CMSSignedDataGenerator;
import org.bouncycastle.cms.CMSTypedData;
import org.bouncycastle.cms.jcajce.JcaSignerInfoGeneratorBuilder;
import org.bouncycastle.operator.ContentSigner;
import org.bouncycastle.operator.DefaultSignatureAlgorithmIdentifierFinder;
import org.bouncycastle.operator.OperatorCreationException;

import org.bouncycastle.operator.jcajce.JcaDigestCalculatorProviderBuilder;

import com.itextpdf.io.codec.Base64;
import com.itextpdf.io.source.ByteArrayOutputStream;


public class CSCSignature extends CreateSignatureBase
{
	
	  String accessToken;
	  String credentialId;
	  String pin;
	  String otp;
	
    /**
     * Initialize the signature creator with a keystore and certificate password.
     *
     * @param keystore the pkcs12 keystore containing the signing certificate
     * @param pin the password for recovering the key
     * @throws KeyStoreException if the keystore has not been initialized (loaded)
     * @throws NoSuchAlgorithmException if the algorithm for recovering the key cannot be found
     * @throws UnrecoverableKeyException if the given password is wrong
     * @throws CertificateException if the certificate is not valid as signing time
     * @throws IOException if no certificate could be found
     */
    public CSCSignature(KeyStore keystore, char[] pin)
            throws KeyStoreException, UnrecoverableKeyException, NoSuchAlgorithmException, CertificateException, IOException
    {
        super(keystore, pin);
    }
    
    private void showSignature( File inFile ) throws Exception
    {
       
            PDDocument document = null;
            try
            {
                document = PDDocument.load( inFile );

                COSDictionary trailer = document.getDocument().getTrailer();
                COSDictionary root = (COSDictionary)trailer.getDictionaryObject( COSName.ROOT );
                COSDictionary acroForm = (COSDictionary)root.getDictionaryObject( COSName.ACRO_FORM );
                COSArray fields = (COSArray)acroForm.getDictionaryObject( COSName.FIELDS );
                for( int i=0; i<fields.size(); i++ )
                {
                    COSDictionary field = (COSDictionary)fields.getObject( i );
                    String type = field.getNameAsString( "FT" );
                    if( "Sig".equals( type ) )
                    {
                        COSDictionary cert = (COSDictionary)field.getDictionaryObject( COSName.V );
                        if( cert != null )
                        {
                            System.out.println( "Certificate found" );
                            System.out.println( "Name=" + cert.getDictionaryObject( COSName.NAME ) );
                            System.out.println( "Modified=" + cert.getDictionaryObject( COSName.getPDFName( "M" ) ) );
                            COSName subFilter = (COSName)cert.getDictionaryObject( COSName.getPDFName( "SubFilter" ) );
                            if( subFilter != null )
                            {
                                if( subFilter.getName().equals( "adbe.x509.rsa_sha1" ) )
                                {
                                    COSString certString = (COSString)cert.getDictionaryObject(
                                        COSName.getPDFName( "Cert" ) );
                                    byte[] certData = certString.getBytes();
                                    CertificateFactory factory = CertificateFactory.getInstance( "X.509" );
                                    ByteArrayInputStream certStream = new ByteArrayInputStream( certData );
                                    Collection certs = factory.generateCertificates( certStream );
                                    System.out.println( "certs=" + certs );
                                }
                                else 
                                {
                                    COSString certString = (COSString)cert.getDictionaryObject(
                                        COSName.CONTENTS );
                                    byte[] certData = certString.getBytes();
                                    CertificateFactory factory = CertificateFactory.getInstance( "X.509" );
                                    ByteArrayInputStream certStream = new ByteArrayInputStream( certData );
                                    Collection certs = factory.generateCertificates( certStream );
                                    System.out.println( "certs=" + certs );
                                }
                                
                            }
                            else
                            {
                                throw new IOException( "Missing subfilter for cert dictionary" );
                            }
                        }
                        else
                        {
                            System.out.println( "Signature found, but no certificate" );
                        }
                    }
                }
            }
            finally
            {
                if( document != null )
                {
                    document.close();
                }
            }
        }
    
    @Override
    public byte[] sign(InputStream content) throws IOException
    {
        try
        {
        	  Certificate[] certificateChain = CSC_API_Utils.GetCertChain(accessToken, credentialId);
        	   CMSSignedDataGenerator gen = new CMSSignedDataGenerator();
               X509Certificate cert = (X509Certificate) certificateChain[0];
               gen.addCertificates(new JcaCertStore(Arrays.asList(certificateChain)));
               
              
               MessageDigest digest = MessageDigest.getInstance("SHA-256");
               byte[] hashBytes = digest.digest(content.readAllBytes());
               String base64Hash = Base64.encodeBytes(hashBytes);
               String   SAD = CSC_API.CredentialsAuthorize(base64Hash,this.accessToken,this.credentialId,this.pin,this.otp);
             String signature = CSC_API.SignatureSignHash(base64Hash,this.accessToken,this.credentialId,SAD);
               final byte[] signedHash =Base64.decode(signature);


               ContentSigner nonSigner = new ContentSigner() {

                @Override
                public byte[] getSignature() {
                    return signedHash;
                }

                @Override
                public OutputStream getOutputStream() {
                    return new ByteArrayOutputStream();
                }

                @Override
                public AlgorithmIdentifier getAlgorithmIdentifier() {
                    return new DefaultSignatureAlgorithmIdentifierFinder().find( "SHA256WithRSA" );
                }
               	};


        
            org.bouncycastle.asn1.x509.Certificate cert2 = org.bouncycastle.asn1.x509.Certificate.getInstance(ASN1Primitive.fromByteArray(cert.getEncoded()));
            JcaSignerInfoGeneratorBuilder sigb = new JcaSignerInfoGeneratorBuilder(new JcaDigestCalculatorProviderBuilder().build());
      
            

            sigb.setDirectSignature( true );
            gen.addSignerInfoGenerator(sigb.build(nonSigner, new X509CertificateHolder(cert2)));
            CMSTypedData msg = new JSON_FORMATS.CMSProcessableInputStream( new ByteArrayInputStream( "not used".getBytes() ) );
           
            CMSSignedData signedData = gen.generate((CMSTypedData)msg, false);
       
            
            if (this.getTsaUrl()!=null) {
            	// ValidationTimeStamp validation = new ValidationTimeStamp(getTsaUrl());
                 //signedData = validation.addSignedTimeStamp(signedData);
            }
           	
            return signedData.getEncoded();
   
        }
        catch (GeneralSecurityException e)
        {
            throw new IOException(e);
        }
        catch (CMSException e)
        {
            throw new IOException(e);
        } catch (OperatorCreationException e) {
			// TODO Auto-generated catch block
        	 throw new IOException(e);
		}
    }

    /**
     * Signs the given PDF file. Alters the original file on disk.
     * @param file the PDF file to sign
     * @throws IOException if the file could not be read or written
     */
    public void signDetached(File file) throws IOException
    {
        signDetached(file, file, null);
    }

    /**
     * Signs the given PDF file.
     * @param inFile input PDF file
     * @param outFile output PDF file
     * @throws IOException if the input file could not be read
     */
    public void signDetached(File inFile, File outFile) throws IOException
    {
        signDetached(inFile, outFile, null);
    }

    /**
     * Signs the given PDF file.
     * @param inFile input PDF file
     * @param outFile output PDF file
     * @param tsaUrl optional TSA url
     * @throws IOException if the input file could not be read
     */
    public void signDetached(File inFile, File outFile, String tsaUrl) throws IOException
    {
        if (inFile == null || !inFile.exists())
        {
            throw new FileNotFoundException("Document for signing does not exist");
        }
        
        setTsaUrl(tsaUrl);

        // sign
        try (FileOutputStream fos = new FileOutputStream(outFile);
                PDDocument doc = PDDocument.load(inFile))
        {
        	
            signDetached(doc, fos);
        }
    }

    public void signDetached(PDDocument document, OutputStream output)
            throws IOException
    {
        
        // create signature dictionary
        PDSignature signature = new PDSignature();
        signature.setFilter(PDSignature.FILTER_ADOBE_PPKLITE);
        signature.setSubFilter(PDSignature.SUBFILTER_ADBE_PKCS7_DETACHED);
        signature.setName("Test Name");
        signature.setLocation("Bucharest, RO");
        signature.setReason("PDFBox Signing");
        signature.setSignDate(Calendar.getInstance());

        if (isExternalSigning())
        {
            document.addSignature(signature);
            ExternalSigningSupport externalSigning =
                    document.saveIncrementalForExternalSigning(output);
    
            byte[] cmsSignature = sign(externalSigning.getContent());
            externalSigning.setSignature(cmsSignature);
            
        }
        else
        {
            SignatureOptions signatureOptions = new SignatureOptions();
            
            
            signatureOptions.setPreferredSignatureSize(SignatureOptions.DEFAULT_SIGNATURE_SIZE * 2);
          
            document.addSignature(signature, this, signatureOptions);

        
            document.saveIncremental(output);
        }
    }

    public static void main(String[] args) throws IOException, GeneralSecurityException
    {
    	
     String tsaUrl="http://timestamp.globalsign.com/scripts/timstamp.dll";
      //load keystore
        KeyStore keystore = KeyStore.getInstance("PKCS12");
        char[] password = "password".toCharArray(); 
        keystore.load(new FileInputStream("E:\\ks"), password);
   

        // sign PDF
        CSCSignature signing = new CSCSignature(keystore, password);
        signing.accessToken="accessTokenHere";
        signing.credentialId="credentialIdHere";
        signing.otp="123456";
        signing.pin="SigningPasswordHere";
        signing.setExternalSigning(true);

        File inFile = new File("E:\\Test_PDFBox.pdf");
        String name = inFile.getName();
        String substring = name.substring(0, name.lastIndexOf('.'));
        
            File outFile = new File(inFile.getParent(), substring + "_signed.pdf");
             signing.signDetached(inFile, outFile, tsaUrl);
    	
        try {
			signing.showSignature(outFile);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
    }

}