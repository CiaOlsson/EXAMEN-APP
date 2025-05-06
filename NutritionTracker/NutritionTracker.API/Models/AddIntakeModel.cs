namespace NutritionTracker.API.Models
{
	public class AddIntakeModel
	{
		public int FoodId { get; set; }
		public float FoodAmount { get; set; } // kcal är per 100g som vanligt så det är så det måste skrivas in eller omvandlas någon stans på vägen..
											   								 
		// eventuellt 									  
		//public string Unit { get; set; } och då kan man ha matematiska formler för att omvända det till 100g. 
	}
}
