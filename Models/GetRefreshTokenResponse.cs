using Newtonsoft.Json;

namespace EncryptionAPIServicesSDK.Models
{
    internal class GetRefreshTokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}