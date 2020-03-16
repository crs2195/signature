package JSON_FORMATS;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;
@JsonIgnoreProperties(ignoreUnknown = true)
public class CredentialsInfoResponse
{
    @JsonProperty("description")
    public String description;
    @JsonProperty("authMode")
    public String authMode;

    @JsonProperty("cert")
    public CredentialsInfoResponseCertificate cert;

  @JsonProperty("multisign")
    public Boolean multisign;

    @JsonProperty("key")
    public CredentialsInfoResponseKey key ;
    public CredentialsInfoResponse() {
 }
    
}

