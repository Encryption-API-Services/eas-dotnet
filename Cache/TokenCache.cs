using EncryptionAPIServicesSDK.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace EncryptionAPIServicesSDK.Cache
{
    public class TokenCache : ITokenCache
    {
        public string token { get; set; }
        public DateTime expirationTime { get; set; }
        public TokenService _tokenService { get; set; }

        public TokenCache()
        {
            SetCache().GetAwaiter().GetResult();
        }

        public async Task<string> GetToken()
        {
            DateTime now = DateTime.UtcNow;
            if (now >= this.expirationTime)
            {
                this.token = await this._tokenService.GetRefreshToken(this.token);
                JwtSecurityToken decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(this.token);
                SetDate(decodedToken);
            }
            return this.token;
        }

        private async Task SetCache()
        {
            _tokenService = new TokenService();
            if (string.IsNullOrEmpty(token))
            {
                try
                {
                    this.token = await _tokenService.GetToken();
                    JwtSecurityToken decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(this.token);
                    SetDate(decodedToken);                
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        private void SetDate(JwtSecurityToken token)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime now = DateTime.UtcNow;
            var expSeconds = token.Claims.First(x => x.Type == "exp").Value;
            this.expirationTime = origin.AddSeconds(double.Parse(expSeconds));
        }

        Task ITokenCache.SetCache()
        {
            throw new NotImplementedException();
        }

        void ITokenCache.SetDate(JwtSecurityToken token)
        {
            throw new NotImplementedException();
        }
    }
}