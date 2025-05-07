using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NutritionTracker.API.Models;
using NutritionTracker.API.Services;

namespace NutritionTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	public class UserController : ControllerBase
    {
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly IAuthService _authService;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAuthService authService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_authService = authService;
		}


		[HttpPost("/signup")]
		[AllowAnonymous]
		public async Task<IActionResult> Signup([FromBody] SignupModel user)
		{
			try
			{
				var identityUser = new IdentityUser
				{
					UserName = user.UserName,
					Email = user.UserEmail
				};

				var result = await _userManager.CreateAsync(identityUser, user.Password);

				if (result.Succeeded)
				{
					return Ok(new 
					{	message = "User created", 
						username = identityUser.UserName, 
						email = identityUser.Email 
					});
				}
				else
				{
					//Logga felmeddelande här? 

					return BadRequest(new 
					{ 
						message = "User creation failed",  
					});
				}
			}
			catch (Exception ex)
			{
				//Logga felmeddelande här? 
				return StatusCode(500, "Something went wrong. Please try again.");
			}
		}

		[HttpPost("/login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] LoginModel loginUser)
		{
			try
			{
				var user = await _userManager.FindByEmailAsync(loginUser.UserEmail);
				if (user == null)
					return Unauthorized("Invalid credentials");

				var result = await _signInManager.PasswordSignInAsync(user, loginUser.Password, false, false);
				if (result.Succeeded)
				{
					var tokenString = await _authService.CreateToken(user);

					return Ok(tokenString);
				}
				else
				{
					return Unauthorized("Invalid credentials");
				}
			}
			catch (Exception ex)
			{
				//Logga felmeddelande här? 
				return StatusCode(500, "Something went wrong. Please try again.");
			}
		}

	}
}
