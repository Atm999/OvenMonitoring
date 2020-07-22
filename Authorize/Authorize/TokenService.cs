using Authorize.Entity;
using Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authorize.Authorize
{
    public class TokenService : ITokenService
    {
        public JwtSetting _jwtSetting;

        public TokenService(IOptions<JwtSetting> option)
        {
            _jwtSetting = option.Value;
        }

        public JwtSetting TokenSetting
        {
            get { return _jwtSetting; }
        }


        public string GetToken(UserInfo user)
        {
            //创建用户身份标识，可按需要添加更多信息
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),     //
                new Claim("user_name", user.user_name.ToString()),
                new Claim("user_role", user.Role)
            };
            try
            {
                //创建令牌
                var token = new JwtSecurityToken(
                        issuer: _jwtSetting.Issuer,
                        audience: _jwtSetting.Audience,
                        signingCredentials: _jwtSetting.Credentials,
                        claims: claims,
                        notBefore: DateTime.Now,//内部是0时区，直接加8
                        expires: DateTime.Now.AddHours(_jwtSetting.ExpireHours)//内部是0时区
                    );
                string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return jwtToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return string.Empty;
        }

        public TokenValidationParameters GetValidationParameters()
        {
            Byte[] SecurityKeyByte = Encoding.UTF8.GetBytes(_jwtSetting.SecurityKey);
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(SecurityKeyByte);
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = _jwtSetting.Issuer,
                ValidAudience = _jwtSetting.Audience,
                IssuerSigningKey = symmetricSecurityKey
            };
            return tokenValidationParameters;
        }

        public UserToken ValidateToken(string AuthToken)
        {
            UserToken res = new UserToken();
            DateTime expire;
            if (string.IsNullOrEmpty(AuthToken) == false)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();
                SecurityToken validatedToken;
                try
                {
                    ClaimsPrincipal principal = tokenHandler.ValidateToken(AuthToken, validationParameters, out validatedToken);
                    JwtSecurityToken JwtVlidatedToken = validatedToken as JwtSecurityToken;
                    expire = validatedToken.ValidTo;
                    Claim claim = JwtVlidatedToken.Claims.First(x => x.Type.Equals("user_name"));
                    if (claim != null)
                    {
                        string UserName = claim.Value;
                        //还未过期
                        if (expire >= DateTime.Now)
                        {
                            
                            res.UserName = claim.Value;
                            res.Role = JwtVlidatedToken.Claims.First(x => x.Type.Equals("user_role")).Value;
                        }
                    }
                }
                catch (Exception ex)//token过期会自动报错
                {
                   
                }
            }
            return res;
        }
    }
}
