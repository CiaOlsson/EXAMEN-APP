using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTracker.Application.Queries.GetIntakesForDate
{
	public class IntakeDTO
	{
		public int FoodId { get; set; }
		public string Name { get; set; }
		public double? Energy_kcal { get; set; }
		public double? Protein { get; set; }
		public double? Fat { get; set; }
		public double? Carbohydrates {  get; set; }
		public double? Fiber { get; set; }
		public double? SugarsTotal { get; set; }
		public double? Salt { get; set; }
		public double? Vitamin_A { get; set; }
		public double? Vitamin_B6 { get; set; }
		public double? Vitamin_B12 { get; set; }
		public double? Vitamin_C { get; set; }
		public double? Vitamin_D { get; set; }
		public double? Vitamin_E { get; set; }
		public double? Vitamin_K { get; set; }
	}
}
