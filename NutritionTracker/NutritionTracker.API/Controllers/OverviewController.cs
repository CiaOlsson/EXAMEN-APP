using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NutritionTracker.API.Models;
using NutritionTracker.Application.Commands.AddIntake;
using NutritionTracker.Application.Queries.GetIntakeForToday;
using System.Security.Claims;

namespace NutritionTracker.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class OverviewController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;

		[HttpPost]
		public async Task<IActionResult> AddIntake([FromBody] AddIntakeModel intake)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

				var command = new AddIntakeCommand
				{
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
		public async Task<IActionResult> GetAllIntakeForToday()
		{
			try
			{
				var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
				
				var query = new GetIntakeForTodayQuery
				{
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
