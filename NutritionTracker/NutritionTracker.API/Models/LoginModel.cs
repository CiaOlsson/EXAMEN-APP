using System.ComponentModel.DataAnnotations;

namespace NutritionTracker.API.Models
{
	public class LoginModel
	{
		[Required]
		public string UserEmail { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
