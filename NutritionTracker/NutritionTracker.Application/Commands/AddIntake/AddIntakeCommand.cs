using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTracker.Application.Commands.AddIntake
{
	public class AddIntakeCommand : IRequest<IntakeAddedDTO>
	{
		public DateOnly DateOfIntake {  get; set; }
		public Guid UserId { get; set; }
		public int FoodId { get; set; }
		public double FoodAmount { get; set; }
	}
}
