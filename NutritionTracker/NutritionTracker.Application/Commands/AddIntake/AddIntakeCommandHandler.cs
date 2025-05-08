using Mediator;
using NutritionTracker.Application.Interfaces;
using NutritionTracker.Domain.Events;

namespace NutritionTracker.Application.Commands.AddIntake
{
	public class AddIntakeCommandHandler : IRequestHandler<AddIntakeCommand, IntakeAddedDTO?>
	{
		private readonly IEventStore _eventStore;
		private readonly IFoodRepository _foodRepo;

		public AddIntakeCommandHandler(IEventStore eventStore, IFoodRepository foodRepo)
		{
			_eventStore = eventStore;
			_foodRepo = foodRepo;
		}

		public async ValueTask<IntakeAddedDTO?> Handle(AddIntakeCommand command, CancellationToken cancellationToken)
		{
			var food = await _foodRepo.GetFoodById(command.FoodId);

			if (food == null)
				return null;

			var intakeAddedEvent = new IntakeAddedEvent
			{
				DateOfIntake = command.DateOfIntake,
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
				DateOfIntake = command.DateOfIntake,
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
