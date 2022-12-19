using Newtonsoft.Json;

namespace EncryptionAPIServicesSDK.Models.Password
{
    internal class BCryptHashPasswordResponse
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
        [JsonProperty("HashedPassword")]
        public string HashedPassword { get; set; }
    }
}