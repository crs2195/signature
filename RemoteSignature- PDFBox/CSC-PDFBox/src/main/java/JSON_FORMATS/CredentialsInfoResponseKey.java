package JSON_FORMATS;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;

@JsonIgnoreProperties(ignoreUnknown = true)
public class CredentialsInfoResponseKey {
    @JsonProperty("algo")
    public String[] algo = {"1.2.840.113549.1.1.1","1.3.14.3.2.29","1.2.840.113549.1.1.11","1.2.840.113549.1.1.13"};

    @JsonProperty("status")
    public String status;

    @JsonProperty("len")
    public int len;
    @JsonProperty("curve")
    public String curve ;
}
