using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NutritionTracker.API.Services
{
	public class AuthService(IConfiguration configuration) : IAuthService
	{
		public async Task<string> CreateToken(IdentityUser user)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName),
			};

			string key = configuration["JWT_key"];

			var secretKey = new SymmetricSecurityKey(Convert.FromBase64String(key));

			var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

			var tokenOptions = new JwtSecurityToken(
				issuer: configuration["JwtSettings:Issuer"],
				audience: configuration["JwtSettings:Audience"],
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: signInCredentials
				);

			var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

			return tokenString;
		}
	}
}
