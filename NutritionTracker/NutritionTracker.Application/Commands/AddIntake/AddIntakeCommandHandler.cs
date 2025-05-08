using Mediator;
using NutritionTracker.Application.Interfaces;
using NutritionTracker.Domain.DomainEntities;
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
			};

			CalculateNutrition(intakeAddedEvent, food, command.FoodAmount);

			await _eventStore.SaveEventAsync(intakeAddedEvent, command.UserId);

			return new IntakeAddedDTO 
			{
				UserId = command.UserId,
				DateOfIntake = intakeAddedEvent.DateOfIntake,
				FoodId = intakeAddedEvent.FoodId,
				Name = intakeAddedEvent.Name,
				Energy_kcal = intakeAddedEvent.Energy_kcal,
				Protein = intakeAddedEvent.Protein,
				Fat = intakeAddedEvent.Fat,
				Carbohydrates = intakeAddedEvent.Carbohydrates
			};
		}

		private void CalculateNutrition(IntakeAddedEvent intake, FoodEntity food, double amount)
		{
			var factor = amount / 100;

			intake.Energy_kcal = food.Energy_kcal * factor;
			intake.Protein = food.Protein_g * factor;
			intake.Fat = food.FatTotal_g * factor;
			intake.Carbohydrates = food.Carbohydrates_g * factor;
		}
	}
}
