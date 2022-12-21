using EncryptionAPIServicesSDK.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace EncryptionAPIServicesSDK.Cache
{
    public interface ITokenCache
    {
        string token { get; set; }
        DateTime expirationTime { get; set; }

        TokenService _tokenService { get; set; }
        Task<string> GetToken();
        Task SetCache();
        void SetDate(JwtSecurityToken token);
    }
}
