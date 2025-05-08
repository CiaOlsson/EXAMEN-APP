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
		public int? Calories { get; set; }
		public double? Protein { get; set; }
		public double? Fat { get; set; }
		public double? Carbohydrates {  get; set; }
	}
}
