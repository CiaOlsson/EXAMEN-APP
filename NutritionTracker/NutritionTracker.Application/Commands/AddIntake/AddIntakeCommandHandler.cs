using Mediator;
using NutritionTracker.Application.Interfaces;
using NutritionTracker.Application.Queries.GetIntakeForToday;
using NutritionTracker.Application.Services;
using NutritionTracker.Domain.DomainEntities;
using NutritionTracker.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTracker.Application.Commands.AddIntake
{
	public class AddIntakeCommandHandler : IRequestHandler<AddIntakeCommand, IntakeAddedDTO>
	{
		private readonly IEventStore _eventStore;
		private readonly IFoodRepository _foodRepo;

		public AddIntakeCommandHandler(IEventStore eventStore, IFoodRepository foodRepo)
		{
			_eventStore = eventStore;
			_foodRepo = foodRepo;
		}

		public async ValueTask<IntakeAddedDTO> Handle(AddIntakeCommand command, CancellationToken cancellationToken)
		{
			var food = await _foodRepo.GetFoodById(command.FoodId);

			var intakeAddedEvent = new IntakeAddedEvent
			{
				FoodId = command.FoodId,
				Name = food.Name,
				Calories = food.Energy_kcal,
				Protein = food.Protein_g,
				Fat = food.FatTotal_g,
				Carbohydrates = food.Carbohydrates_g
			};

			await _eventStore.SaveEventAsync(intakeAddedEvent, command.UserId);

			return new IntakeAddedDTO 
			{
				UserId = command.UserId,
				FoodId = command.FoodId,
				Name = food.Name,
				Calories = food.Energy_kcal,
				Protein = food.Protein_g,
				Fat = food.FatTotal_g,
				Carbohydrates = food.Carbohydrates_g
			};
		}
	}
}
