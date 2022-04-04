using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserPanel.Shared.Configurations;
using UserPanel.Shared.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserPanel.Core.Models;
using UserPanel.Core.Services;
using UserPanel.Core.Dtos;

namespace UserPanel.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly CustomTokenOption _tokenOption;

        public TokenService(IOptions<CustomTokenOption> options)
        {
            _tokenOption = options.Value;
        }

        private IEnumerable<Claim> GetClaims(AppUser appUser, List<String> audiences)
        {
            var userList = new List<Claim> {                
                new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                new Claim("UserId", appUser.Id),
                new Claim(type: "Idd", value: appUser.Id),
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim(ClaimTypes.Name, appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            return userList;
        }

        public async Task<TokenDto> CreateToken(AppUser userApp)
        {
            var accessTokenExpiration = DateTime.Now.AddDays(_tokenOption.AccessTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaims(userApp, _tokenOption.Audience),
                signingCredentials: signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var tokenDto = new TokenDto
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration,
            };

            return tokenDto;
        }
    }
}
