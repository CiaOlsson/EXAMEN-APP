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
				Carbohydrates = intakeAddedEvent.Carbohydrates,
				Fiber = intakeAddedEvent.Fiber,
				SugarsTotal = intakeAddedEvent.SugarsTotal,
				Salt = intakeAddedEvent.Salt,
				Vitamin_A = intakeAddedEvent.Vitamin_A,
				Vitamin_B6 = intakeAddedEvent.Vitamin_B6,
				Vitamin_B12 = intakeAddedEvent.Vitamin_B12,
				Vitamin_C = intakeAddedEvent.Vitamin_C,
				Vitamin_D = intakeAddedEvent.Vitamin_D,
				Vitamin_E = intakeAddedEvent.Vitamin_E,
				Vitamin_K = intakeAddedEvent.Vitamin_K
			};
		}

		private void CalculateNutrition(IntakeAddedEvent intake, FoodEntity food, double amount)
		{
			var factor = amount / 100;

			intake.Energy_kcal = food.Energy_kcal * factor;
			intake.Protein = food.Protein_g * factor;
			intake.Fat = food.FatTotal_g * factor;
			intake.Carbohydrates = food.Carbohydrates_g * factor;
			intake.Fiber = food.Fiber_g * factor;
			intake.SugarsTotal = food.SugarsTotal_g * factor;
			intake.Salt = food.Salt_g * factor;
			intake.Vitamin_A = food.Vitamin_A_Re_ug * factor;
			intake.Vitamin_B6 = food.Vitamin_B6_mg * factor;
			intake.Vitamin_B12 = food.Vitamin_B12_ug * factor;
			intake.Vitamin_C = food.Vitamin_C_mg * factor;
			intake.Vitamin_D = food.Vitamin_D_ug * factor;
			intake.Vitamin_E = food.Vitamin_E_mg * factor;
			intake.Vitamin_K = food.Vitamin_K_ug * factor;
		}
	}
}
