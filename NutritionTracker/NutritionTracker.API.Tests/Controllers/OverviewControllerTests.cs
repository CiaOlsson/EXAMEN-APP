using FakeItEasy;
using FluentAssertions;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NutritionTracker.API.Controllers;
using NutritionTracker.API.Models;
using NutritionTracker.Application.Commands.AddIntake;
using NutritionTracker.Application.Queries.GetIntakesForDate;
using System.Security.Claims;

namespace NutritionTracker.API.Tests.Controllers
{
	public class OverviewControllerTests
	{
		private IMediator _mediator;
		private ClaimsPrincipal _user;
		private OverviewController _sut;

		private readonly Guid UserId = Guid.NewGuid();
		private readonly DateOnly DateOfIntake = DateOnly.Parse("2025-02-02");

		[SetUp]
		public void SetUp()
		{
			_mediator = A.Fake<IMediator>();
			_sut = new OverviewController(_mediator);

			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.NameIdentifier, UserId.ToString()),
				new Claim(ClaimTypes.Name, "Cissi")
			};

			var identity = new ClaimsIdentity(claims, "TestAuthType");

			_user = new ClaimsPrincipal(identity);

			_sut.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = _user
				}
			};
		}

		[Test]
		public async Task AddIntake_ShouldReturnIntakeAddedDTO_WhenCommandIsCorrect()
		{
			//Arrange
			var addIntake = new AddIntakeModel
			{
				DateOfIntake = DateOfIntake,
				FoodId = 21,
				FoodAmount = 1
			};

			var intakeAdded = new IntakeAddedDTO
			{
				DateOfIntake = DateOfIntake,
				UserId = UserId,
				FoodId = 21,
				Name = "Ananas",
			};

			A.CallTo(() => _mediator.Send(A<AddIntakeCommand>.That.Matches(command =>
			command.DateOfIntake == DateOfIntake &&
			command.UserId == UserId &&
			command.FoodId == addIntake.FoodId &&
			command.FoodAmount == addIntake.FoodAmount), A<CancellationToken>._)).Returns(intakeAdded);

			//Act
			var result = await _sut.AddIntake(addIntake);

			//Assert
			result.Should().BeOfType<OkObjectResult>();
			var value = (result as OkObjectResult)!.Value;

			value.Should().Satisfy<IntakeAddedDTO>(i =>
			{
				i.DateOfIntake.Should().Be(DateOfIntake);
				i.Name.Should().Be("Ananas");
				i.UserId.Should().Be(UserId);
				i.FoodId.Should().Be(21);
			});
		}

		[Test]
		public async Task GetIntakesForDate_ShouldReturnListOfIntakes_WhenUserHasAddedIntakes() 
		{
			//Arrange
			var query = new GetIntakesForDateQuery
			{
				DateOfIntake = DateOfIntake,
				UserId = UserId,
			};

			var listOfIntakes = new List<IntakeDTO>
			{
				new()
				{
					FoodId = 21,
					Name = "Ananas"
				},
				new()
				{
					FoodId = 431,
					Name = "Äpple"
				}
			};

			A.CallTo(() => _mediator.Send(A<GetIntakesForDateQuery>.That.Matches(query => 
			query.UserId == UserId && 
			query.DateOfIntake == DateOfIntake), A<CancellationToken>._))
				.Returns(listOfIntakes);

			//Act
			var result = await _sut.GetIntakesForDate(DateOfIntake);

			//Assert
			result.Should().BeOfType<OkObjectResult>();
			var value = (result as OkObjectResult)!.Value;

			value.Should().Satisfy<List<IntakeDTO>>(list =>
			{
				list.Count.Should().Be(2);
				list.First().Name.Should().Be("Ananas");
				list.First().FoodId.Should().Be(21);
			});
		}

	}
}
