using EncryptionAPIServicesSDK.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EncryptionAPIServicesSDK.Services
{
    public class TokenService
    {
        private readonly HttpClientWrapper _httpClient;
        public TokenService()
        {
            this._httpClient = HttpClientWrapper.Instance;
        }

        /// <summary>
        /// Calls the TokenController in the main API to retrieve the token by the user's api key.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetToken()
        {
            string token = string.Empty;
            if (string.IsNullOrEmpty(EASConfiguration.ApiKey))
            {
                throw new Exception("Please set your ApiKey provided in your dashboard to the EASConfiguration object.");
            }
            string url = EASConfiguration.BaseUrl + String.Format("token");
            this._httpClient.DefaultRequestHeaders.Add("ApiKey", EASConfiguration.ApiKey);
            HttpResponseMessage response = await this._httpClient.GetAsync(url);
            bool wasSuccessful = response.IsSuccessStatusCode;
            if (wasSuccessful)
            {
                GetTokenResponse responseBody = JsonConvert.DeserializeObject<GetTokenResponse>(await response.Content.ReadAsStringAsync());
                if (responseBody.Token != null)
                {
                    token = responseBody.Token;
                }
            }
            return token;
        }
    }
}