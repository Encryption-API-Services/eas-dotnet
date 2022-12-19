using EncryptionAPIServicesSDK.Models.Password;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionAPIServicesSDK.Services
{
    public class PasswordService
    {
        private readonly HttpClientWrapper _httpClient;
        public PasswordService()
        {
            this._httpClient = HttpClientWrapper.Instance;
        }
        public async Task<string> BcryptHashPassword(string token, string password)
        {
            string hashedPassword = string.Empty;
            if (string.IsNullOrEmpty(EASConfiguration.ApiKey))
            {
                throw new Exception("Please set your ApiKey provided in your dashboard to the EASConfiguration object.");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("No password was passed into BCryptHashPassword to hash");
            }
            if (!this._httpClient.DefaultRequestHeaders.Contains("ApiKey"))
            {
                this._httpClient.DefaultRequestHeaders.Add("ApiKey", EASConfiguration.ApiKey);

            }
            this._httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", token));
            string url = EASConfiguration.BaseUrl + "Password/BCryptEncrypt";
            BCryptHashPasswordRequest requestBody = new BCryptHashPasswordRequest()
            {
                Password = password
            };
            StringContent jsonData = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await this._httpClient.PostAsync(url, jsonData);
            BCryptHashPasswordResponse parsedBody = JsonConvert.DeserializeObject<BCryptHashPasswordResponse>(await httpResponse.Content.ReadAsStringAsync());
            if (parsedBody.HashedPassword != null)
            {
                hashedPassword = parsedBody.HashedPassword;
            }
            return hashedPassword;
        }
    }
}
