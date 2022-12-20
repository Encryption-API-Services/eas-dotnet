﻿using EncryptionAPIServicesSDK.Models.Password;
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
