using System.ComponentModel.DataAnnotations;

namespace NutritionTracker.API.Models
{
	public class AddIntakeModel
	{
		public DateOnly DateOfIntake { get; set; }
		[Range(1, int.MaxValue, ErrorMessage = "FoodId must be greater than 0")]
		public int FoodId { get; set; }
		[Range(0.01, float.MaxValue, ErrorMessage = "FoodAmount must be greater than 0")]
		public float FoodAmount { get; set; } // kcal är per 100g som vanligt så det är så det måste skrivas in eller omvandlas någon stans på vägen..
		// eventuellt 									  
		//public string Unit { get; set; } och då kan man ha matematiska formler för att omvända det till 100g. 
	}
}
