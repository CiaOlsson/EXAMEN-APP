using FakeItEasy;
using FluentAssertions;
using NUnit.Framework.Constraints;
using NutritionTracker.Application.Interfaces;
using NutritionTracker.Application.Queries.GetIntakesForDate;
using NutritionTracker.Domain.DomainEntities;

namespace NutritionTracker.Application.Tests.Queries
{
	public class GetIntakesForDateQueryHandlerTests
	{
		private IEventStore _eventStore;

		private readonly Guid UserId = Guid.NewGuid();
		private readonly Guid FirstEventId = Guid.NewGuid();

		[SetUp]
		public void SetUp()
		{
			_eventStore = A.Fake<IEventStore>();
		}


		[Test]
		public async Task Handle_ShouldOnlyReturnIntakesWithCorrectDate_WhenIntakesExists()
		{
			//Arrange
			var query = new GetIntakesForDateQuery
			{
				DateOfIntake = DateOnly.Parse("2025-05-08"),
				UserId = UserId,
			};

			var events = new List<DomainEvent>
			{
				new DomainEvent
				{
					Id = FirstEventId,
					Version = 1,
					EventType = "IntakeAddedEvent",
					Data = "{ \"DateOfIntake\": \"2025-05-08\", \"FoodId\": 2, \"Name\": \"Päron\" }",
					Timestamp = DateTime.Parse("2025-05-08 19:30:00"),
					UserId = UserId
				},
				new DomainEvent
				{
					Id = Guid.NewGuid(),
					Version = 2,
					EventType = "IntakeAddedEvent",
					Data = "{ \"DateOfIntake\": \"2025-05-06\", \"FoodId\": 3, \"Name\": \"Äpple\" }",
					Timestamp = DateTime.Parse("2025-05-08 19:34:10"),
					UserId = UserId
				},
			};

			A.CallTo(() => _eventStore.GetEventsAsync(query.UserId)).Returns(Task.FromResult(events));

			//Act
			var result = await Sut.Handle(query, CancellationToken.None);

			//Assert
			result.Should().BeOfType<List<IntakeDTO>>();
			result.Count().Should().Be(1);
			result.First().FoodId.Should().Be(2);
			result.First().Name.Should().Be("Päron");
		}

		[Test]
		public async Task Handle_ShouldReturnAnEmptyList_WhenNoIntakesWereFound()
		{
			//Arrange
			var query = new GetIntakesForDateQuery
			{
				DateOfIntake = DateOnly.Parse("2025-05-08"),
				UserId = UserId,
			};

			A.CallTo(() => _eventStore.GetEventsAsync(query.UserId)).Returns(Task.FromResult( new List<DomainEvent>()));

			//Act
			var result = await Sut.Handle(query, CancellationToken.None);

			//Assert
			result.Count.Should().Be(0);
		}

		private GetIntakesForDateQueryHandler Sut => new GetIntakesForDateQueryHandler(_eventStore);
	}
}
