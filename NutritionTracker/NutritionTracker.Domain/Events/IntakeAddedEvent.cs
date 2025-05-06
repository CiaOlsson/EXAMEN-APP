using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTracker.Domain.Events
{
	public class IntakeAddedEvent
	{
		public int FoodId { get; set; }
		public string Name { get; set; }
		public int? Calories { get; set; }
		public double? Protein { get; set; }
		public double? Fat { get; set; }
		public double? Carbohydrates { get; set; }

		// de andra jag lägger till får vara nullable.


		//public DateTime Timestamp { get; set; } 
		//kan vara bra om jag senare vill att man ska kunna lägga in mat i efterhand, tex flera dagar senare.

	}
}
