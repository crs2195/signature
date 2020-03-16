package JSON_FORMATS;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;
@JsonIgnoreProperties(ignoreUnknown = true)
public class CredentialsInfoResponseCertificate {

    @JsonProperty("certificates")
    public String[] certificates ;
    @JsonProperty("status")
    public String status;
    
}
