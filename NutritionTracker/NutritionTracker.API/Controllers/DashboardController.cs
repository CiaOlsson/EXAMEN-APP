using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutritionTracker.API.Models;
using NutritionTracker.Application.Commands.AddIntake;
using NutritionTracker.Application.Queries.GetIntakesForDate;
using System.Security.Claims;

namespace NutritionTracker.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class DashboardController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> AddIntake([FromBody] AddIntakeModel intake)
		{
			try
			{
				var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

				var command = new AddIntakeCommand
				{
					DateOfIntake = intake.DateOfIntake,
					UserId = userId,
					FoodId = intake.FoodId,
					FoodAmount = intake.FoodAmount
				};

				var result = await _mediator.Send(command); 

				return Ok(result); 
			}
			catch (Exception ex)
			{
				//Logga felmeddelande här? 
				return StatusCode(500, "Something went wrong. Please try again.");
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetIntakesForDate([FromQuery] DateOnly date)
		{
			try
			{
				var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
				
				var query = new GetIntakesForDateQuery
				{
					DateOfIntake = date,
					UserId = userId
				};

				var result = await _mediator.Send(query);
				
				return Ok(result);
			}
			catch (Exception ex)
			{
				//Logga felmeddelande här? 
				return StatusCode(500, "Something went wrong. Please try again.");
			}
		}
	}
}
