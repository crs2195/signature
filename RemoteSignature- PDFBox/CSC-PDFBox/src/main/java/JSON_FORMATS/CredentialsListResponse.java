package JSON_FORMATS;
import com.fasterxml.jackson.annotation.JsonProperty;

public class CredentialsListResponse {
    @JsonProperty("credentialIDs")
    public String[] CredentialIds;

}
