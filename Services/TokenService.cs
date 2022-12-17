using System;
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
        public string GetToken()
        {
            string token = string.Empty;
            if (string.IsNullOrEmpty(EASConfiguration.ApiToken))
            {
                throw new Exception("Please set your ApiToken provided in your dashboard to the EASConfiguration object.");
            }
            return token;
        }

        /// <summary>
        /// Calls the TokenController in the main API to retrieve the token by the user's api key in an asynchronous fashion.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetTokenAsync()
        {
            string token = string.Empty;
            if (string.IsNullOrEmpty(EASConfiguration.ApiToken))
            {
                throw new Exception("Please set your ApiToken provided in your dashboard to the EASConfiguration object.");
            }
            return token;
        }
    }
}