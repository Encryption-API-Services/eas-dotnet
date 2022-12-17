using Newtonsoft.Json;

namespace EncryptionAPIServicesSDK.Models
{
    public class GetTokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
