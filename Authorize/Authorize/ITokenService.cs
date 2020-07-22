using Authorize.Entity;
using Entity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorize.Authorize
{
    public interface ITokenService
    {
        JwtSetting TokenSetting { get; }
        string GetToken(UserInfo user);
        TokenValidationParameters GetValidationParameters();
        UserToken ValidateToken(string AuthToken);
    }
}
