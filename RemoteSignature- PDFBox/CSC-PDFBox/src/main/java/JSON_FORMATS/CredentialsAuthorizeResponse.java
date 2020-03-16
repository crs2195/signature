package JSON_FORMATS;

import com.fasterxml.jackson.annotation.JsonProperty;

public class CredentialsAuthorizeResponse {
    @JsonProperty("SAD")
    public String SAD;

    @JsonProperty("expiresIn")
    public int ExpiresIn;
}
