package JSON_FORMATS;

import com.fasterxml.jackson.annotation.JsonProperty;


public class AuthLoginResponse {
    @JsonProperty("expires_in")
    public int ExpiresIn;

    @JsonProperty("refresh_token")
    public String RefreshToken;

    @JsonProperty("access_token")
    public String AccessToken;
}
