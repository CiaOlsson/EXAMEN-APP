using System.ComponentModel.DataAnnotations;

namespace NutritionTracker.API.Models
{
	public class SignupModel
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string UserEmail { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
