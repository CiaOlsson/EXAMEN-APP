using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTracker.Application.Queries.GetIntakeForToday
{
	public class GetIntakeForTodayQuery : IRequest<List<GetIntakeForTodayDTO>>
	{
		public Guid UserId { get; set; }

		//lägg till date här om jag vill utöka med att man ska kunna hitta på ett speciellt datum? eller nej föresten jag kanske gör en annan query för historik?? 
	}
}
