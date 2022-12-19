using EncryptionAPIServicesSDK.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
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
        /// Calls the TokenController in the main API to retrieve the token by the user's api key
        /// </summary>
        /// <returns>Returns an empty string if there was an error on the API side. 
        /// If successful returns the token in encoded string format
        /// </returns>
        public async Task<string> GetToken()
        {
            string token = string.Empty;
            if (string.IsNullOrEmpty(EASConfiguration.ApiKey))
            {
                throw new Exception("Please set your ApiKey provided in your dashboard to the EASConfiguration object.");
            }
            string url = EASConfiguration.BaseUrl + "token";
            if (!this._httpClient.DefaultRequestHeaders.Contains("ApiKey"))
            {
                this._httpClient.DefaultRequestHeaders.Add("ApiKey", EASConfiguration.ApiKey);
            }
            HttpResponseMessage response = await this._httpClient.GetAsync(url);
            GetTokenResponse responseBody = JsonConvert.DeserializeObject<GetTokenResponse>(await response.Content.ReadAsStringAsync());
            token = responseBody.Token;
            return token;
        }

        /// <summary>
        /// User passes in a token and calls the main API to create a new token if it expired.
        /// Returns the same token if the token is still valid.
        /// </summary>
        /// <returns>Returns an empty string if there was an error on the API side. 
        /// If successful returns the token in encoded string format.
        /// </returns>
        public async Task<string> GetRefreshToken(string oldToken)
        {
            string token = string.Empty;
            if (string.IsNullOrEmpty(EASConfiguration.ApiKey))
            {
                throw new Exception("Please set your ApiKey provided in your dashboard to the EASConfiguration object.");
            }
            if (string.IsNullOrEmpty(oldToken))
            {
                throw new Exception("Please pass in an expired token to replace the existing token");
            }
            string url = EASConfiguration.BaseUrl + "token/RefreshToken";
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(oldToken);
            HttpResponseMessage response = await this._httpClient.GetAsync(url);
            GetRefreshTokenResponse responseBody = JsonConvert.DeserializeObject<GetRefreshTokenResponse>(await response.Content.ReadAsStringAsync());
            token = responseBody.Token;
            return token;
        }
    }
}