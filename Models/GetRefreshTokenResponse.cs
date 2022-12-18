using Newtonsoft.Json;

namespace EncryptionAPIServicesSDK.Models
{
    public class GetRefreshTokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}