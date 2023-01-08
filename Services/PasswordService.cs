using EncryptionAPIServicesSDK.Models.Password;
using EncryptionAPIServicesSDK.Password;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public async Task<string> ScryptHashPassword(string token, string passwordToHash)
        {
            string hashedPassword = string.Empty;
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("There was no token provided to Argon2HashPassword to hash your password");
            }
            if (string.IsNullOrEmpty(EASConfiguration.ApiKey))
            {
                throw new Exception("Please set your ApiKey provided in your dashboard to the Argon2HashPassword object.");
            }
            if (string.IsNullOrEmpty(passwordToHash))
            {
                throw new Exception("No password was passed into Argon2HashPassword to hash");
            }
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
            string url = EASConfiguration.BaseUrl + "Password/SCryptEncrypt";
            ScryptHashPasswordRequest requestBody = new ScryptHashPasswordRequest() { PasswordToHash = passwordToHash };
            StringContent jsonData = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await this._httpClient.PostAsync(url, jsonData);
            ScryptHashPasswordResponse parsedBody = JsonConvert.DeserializeObject<ScryptHashPasswordResponse>(await httpResponse.Content.ReadAsStringAsync());
            if (parsedBody.HashedPassword != null)
            {
                hashedPassword = parsedBody.HashedPassword;
            }
            return hashedPassword;
        }
        public async Task<bool> ScryptVerifyPassword(string token, string hashedPassword, string password)
        {
            bool isValid = false;
            if (string.IsNullOrEmpty(EASConfiguration.ApiKey))
            {
                throw new Exception("Please set your ApiKey provided in your dashboard to the EASConfiguration object.");
            }
            if (string.IsNullOrEmpty(hashedPassword))
            {
                throw new Exception("No hashed password was passed into ScryptVerifyPassword to verify.");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("No password was passed into ScryptVerifyPassword to verify");
            }
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("There was no token provided to verify ScryptVerifyPassword");
            }
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
            string url = EASConfiguration.BaseUrl + "Password/SCryptVerify";
            ScryptVerifyRequest requestBody = new ScryptVerifyRequest() { HashedPassword = hashedPassword, Password = password };
            var jsonBody = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await this._httpClient.PostAsync(url, jsonBody);
            ScryptVerifyResponse parsedResponse = JsonConvert.DeserializeObject<ScryptVerifyResponse>(await response.Content.ReadAsStringAsync());
            return parsedResponse.IsValid;
        }
        public async Task<string> Argon2HashPassword(string token, string passwordToHash)
        {
            string hashedPassword = string.Empty;
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("There was no token provided to Argon2HashPassword to hash your password");
            }
            if (string.IsNullOrEmpty(EASConfiguration.ApiKey))
            {
                throw new Exception("Please set your ApiKey provided in your dashboard to the Argon2HashPassword object.");
            }
            if (string.IsNullOrEmpty(passwordToHash))
            {
                throw new Exception("No password was passed into Argon2HashPassword to hash");
            }
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
            string url = EASConfiguration.BaseUrl + "Password/Argon2Hash";
            Argon2HashPasswordRequest requestBody = new Argon2HashPasswordRequest() { PasswordToHash = passwordToHash };
            StringContent jsonData = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await this._httpClient.PostAsync(url, jsonData);
            Argon2HashPasswordResponse parsedBody = JsonConvert.DeserializeObject<Argon2HashPasswordResponse>(await httpResponse.Content.ReadAsStringAsync());
            if (parsedBody.HashedPassword != null)
            {
                hashedPassword = parsedBody.HashedPassword;
            }
            return hashedPassword;
        }

        public async Task<bool> Argon2VerifyPassword(string token, string hashedPassword, string password)
        {
            bool isValid = false;
            if (string.IsNullOrEmpty(EASConfiguration.ApiKey))
            {
                throw new Exception("Please set your ApiKey provided in your dashboard to the EASConfiguration object.");
            }
            if (string.IsNullOrEmpty(hashedPassword))
            {
                throw new Exception("No hashed password was passed into Argon2VerifyPassword to verify.");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("No password was passed into Argon2VerifyPassword to verify");
            }
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("There was no token provided to verify Argon2VerifyPassword");
            }
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
            string url = EASConfiguration.BaseUrl + "Password/Argon2Verify";
            Argon2VerifyRequest requestBody = new Argon2VerifyRequest() { HashedPassword = hashedPassword, Password = password };
            var jsonBody = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await this._httpClient.PostAsync(url, jsonBody);
            Argon2VerifyResponse parsedResponse = JsonConvert.DeserializeObject<Argon2VerifyResponse>(await response.Content.ReadAsStringAsync());
            return parsedResponse.IsValid;
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
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
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

        public async Task<bool> BCryptVerifyPassword(string token, string hashedPassword, string password)
        {
            bool isValid = false;
            if (string.IsNullOrEmpty(EASConfiguration.ApiKey))
            {
                throw new Exception("Please set your ApiKey provided in your dashboard to the EASConfiguration object.");
            }
            if (string.IsNullOrEmpty(hashedPassword))
            {
                throw new Exception("No hashed password was passed into BCryptVerifyPasssword to verify.");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("No password was passed into BCryptVerifyPassword to verify");
            }
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("There was no token provided to verify BCryptVerifyPassword");
            }
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
            string url = EASConfiguration.BaseUrl + "Password/BcryptVerify";
            BCryptVerifyRequest requestBody = new BCryptVerifyRequest()
            {
                Password = password,
                HashedPassword = hashedPassword
            };
            var jsonBody = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await this._httpClient.PostAsync(url, jsonBody);
            BCryptVerifyResponse parsedResponse = JsonConvert.DeserializeObject<BCryptVerifyResponse>(await response.Content.ReadAsStringAsync());
            return parsedResponse.IsValid;
        }
    }
}
