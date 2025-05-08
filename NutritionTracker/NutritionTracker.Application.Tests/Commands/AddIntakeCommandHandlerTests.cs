using FakeItEasy;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NutritionTracker.Application.Commands.AddIntake;
using NutritionTracker.Application.Interfaces;
using NutritionTracker.Domain.DomainEntities;
using NutritionTracker.Domain.Events;
using System.Xml.Linq;

namespace NutritionTracker.Application.Tests.Commands
{
	public class AddIntakeCommandHandlerTests
	{
		private IEventStore _eventStore;
		private IFoodRepository _foodRepo;

		private readonly Guid UserId = Guid.NewGuid();

		[SetUp]
		public void SetUp()
		{
			_eventStore = A.Fake<IEventStore>();
			_foodRepo = A.Fake<IFoodRepository>();
		}

		[Test]
		public async Task Handle_ShouldReturnIntakeAddedDTO_WhenCommandIsCorrect()
		{
			//Arrange
			var command = new AddIntakeCommand
			{
				UserId = UserId,
				FoodId = 21,
				FoodAmount = 200
			};

			var food = new FoodEntity
			{
				Name = "Apple",
				Energy_kcal = 95,
				Protein_g = 1,
				FatTotal_g = 1,
				Carbohydrates_g = 1
			};

			A.CallTo(() => _foodRepo.GetFoodById(command.FoodId)).Returns(food);

			var factor = command.FoodAmount / 100.0;

			var expectedEvent = new IntakeAddedEvent
			{
				FoodId = command.FoodId,
				Name = food.Name,
				Energy_kcal = food.Energy_kcal * factor,
				Protein = food.Protein_g * factor,
				Fat = food.FatTotal_g * factor,
				Carbohydrates = food.Carbohydrates_g * factor
			};

			//Act
			var result = await Sut.Handle(command, CancellationToken.None);

			//Assert
			A.CallTo(() => _eventStore.SaveEventAsync(A<IntakeAddedEvent>.That
				.Matches(e =>
				e.FoodId == expectedEvent.FoodId &&
				e.Name == expectedEvent.Name &&
				e.Energy_kcal == expectedEvent.Energy_kcal &&
				e.Protein == expectedEvent.Protein &&
				e.Fat == expectedEvent.Fat &&
				e.Carbohydrates == expectedEvent.Carbohydrates
				), command.UserId))
				.MustHaveHappenedOnceExactly();

			result.Should().NotBeNull();
			result.Should().BeOfType<IntakeAddedDTO>();
			result.UserId.Should().Be(command.UserId);
			result.FoodId.Should().Be(command.FoodId);
			result.Name.Should().Be("Apple");
			result.Energy_kcal.Should().Be(190);
			result.Protein.Should().Be(2);
			result.Fat.Should().Be(2);
			result.Carbohydrates.Should().Be(2);
		}

		[Test]
		public async Task Handle_ShouldReturnNull_WhenFoodIdIsNotFound()
		{
			//Arrange
			var command = new AddIntakeCommand
			{
				UserId = UserId,
				FoodId = 21,
				FoodAmount = 1
			};

			A.CallTo(() => _foodRepo.GetFoodById(command.FoodId)).Returns(Task.FromResult<FoodEntity>(null));

			//Act
			var result = await Sut.Handle(command, CancellationToken.None);

			//Assert
			result.Should().BeNull();
		}

		private AddIntakeCommandHandler Sut => new AddIntakeCommandHandler(_eventStore, _foodRepo);
	}
}
