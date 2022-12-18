using Newtonsoft.Json;

namespace EncryptionAPIServicesSDK.Models
{
    internal class GetTokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
