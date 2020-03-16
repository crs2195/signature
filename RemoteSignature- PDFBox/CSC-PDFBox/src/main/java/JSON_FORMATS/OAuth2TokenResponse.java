package JSON_FORMATS;

import com.fasterxml.jackson.annotation.JsonProperty;


public class OAuth2TokenResponse {
    @JsonProperty("expires_in")
    public int ExpiresIn;

    @JsonProperty("refresh_token")
    public String RefreshToken;

    @JsonProperty("access_token")
    public String AccessToken;
    @JsonProperty("token_type")
    public String TokenType;
    
}
