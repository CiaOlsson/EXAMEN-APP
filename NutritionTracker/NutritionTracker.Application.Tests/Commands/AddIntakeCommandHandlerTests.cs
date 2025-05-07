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
				FoodAmount = 1
			};

			var food = new FoodEntity
			{
				Name = "Apple",
				Energy_kcal = 95,
				Protein_g = 0.5,
				FatTotal_g = 0.3,
				Carbohydrates_g = 25
			};

			A.CallTo(() => _foodRepo.GetFoodById(command.FoodId)).Returns(food);

			//Act
			var result = await Sut.Handle(command, CancellationToken.None);

			//Assert
			A.CallTo(() => _eventStore.SaveEventAsync(A<IntakeAddedEvent>.That
				.Matches(e =>
				e.FoodId == command.FoodId &&
				e.Name == food.Name &&
				e.Calories == food.Energy_kcal &&
				e.Protein == food.Protein_g &&
				e.Fat == food.FatTotal_g &&
				e.Carbohydrates == food.Carbohydrates_g
				), command.UserId))
				.MustHaveHappenedOnceExactly();

			result.Should().NotBeNull();
			result.Should().BeOfType<IntakeAddedDTO>();
			result.UserId.Should().Be(command.UserId);
			result.FoodId.Should().Be(command.FoodId);
			result.Name.Should().Be(food.Name);
			result.Calories.Should().Be(food.Energy_kcal);
			result.Protein.Should().Be(food.Protein_g);
			result.Fat.Should().Be(food.FatTotal_g);
			result.Carbohydrates.Should().Be(food.Carbohydrates_g);
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
