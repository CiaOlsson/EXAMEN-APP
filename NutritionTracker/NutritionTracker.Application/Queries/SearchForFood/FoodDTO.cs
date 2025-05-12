using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTracker.Application.Queries.SearchForFood
{
	public class FoodDTO
	{
		public int FoodId { get; set; }
		public string Name { get; set; }
		public double? Energy_kcal { get; set; }
	}
}
