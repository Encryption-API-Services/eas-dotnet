using EncryptionAPIServicesSDK.Models.Password;
using EncryptionAPIServicesSDK.Models.Rsa;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionAPIServicesSDK.Services
{
    public class RsaService
    {
        private readonly HttpClientWrapper _httpClient;
        public RsaService()
        {
            this._httpClient = HttpClientWrapper.Instance;
        }

        public async Task<RsaGetKeyPairResponse> GetRsaKeys(string token, int keySize)
        {
            if (string.IsNullOrEmpty(EASConfiguration.ApiKey))
            {
                throw new Exception("Please set your ApiKey provided in your dashboard to the Argon2HashPassword object.");
            }
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("You need to pass in a joke to get an RSA key pair");
            }
            if (keySize != 1024 && keySize != 2048 && keySize != 4096)
            {
                throw new Exception("You need to specify a valid key size to generate RSA keys");
            }
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
            string url = EASConfiguration.BaseUrl + String.Format("Rsa/GetKeyPair?keySize={0}", keySize.ToString());
            GetRsaKeyPairRequest requestBody = new GetRsaKeyPairRequest() { KeySize = keySize };
            StringContent jsonData = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await this._httpClient.GetAsync(url);
            return JsonConvert.DeserializeObject<RsaGetKeyPairResponse>(await httpResponse.Content.ReadAsStringAsync());
        }
    }
}