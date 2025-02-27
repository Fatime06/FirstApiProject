using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication4.Entities;

namespace WebApplication4.Services
{
	public class JwtService
	{
		public string GetToken(AppUser user, IList<string> roles, JwtSetting jwt)
		{
			var claims = new List<Claim>
	{
		new Claim(ClaimTypes.NameIdentifier,user.Id),
		new Claim(ClaimTypes.Email,user.Email),
		new Claim("FullName",user.FullName),
		new Claim(ClaimTypes.Name,user.UserName)
	};
			claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				issuer: jwt.Issuer,
				audience: jwt.Audience,
				claims: claims,
				expires: DateTime.Now.AddMinutes(10),
				signingCredentials: creds
				);
			var tokenData = new JwtSecurityTokenHandler().WriteToken(token);
			return tokenData;
		}
	}
}
