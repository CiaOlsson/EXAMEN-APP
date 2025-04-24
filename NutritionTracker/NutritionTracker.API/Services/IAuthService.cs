using Microsoft.AspNetCore.Identity;

namespace NutritionTracker.API.Services
{
	public interface IAuthService
	{
		Task<string> CreateToken(IdentityUser user);
	}
}