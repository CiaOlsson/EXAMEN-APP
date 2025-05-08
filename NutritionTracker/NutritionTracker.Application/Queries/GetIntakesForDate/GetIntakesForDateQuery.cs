using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTracker.Application.Queries.GetIntakesForDate
{
	public class GetIntakesForDateQuery : IRequest<List<IntakeDTO>>
	{
		public Guid UserId { get; set; }
		public DateOnly DateOfIntake {  get; set; } 
	}
}
