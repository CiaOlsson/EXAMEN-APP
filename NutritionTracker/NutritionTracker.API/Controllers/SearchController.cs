﻿using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NutritionTracker.Application.Queries.GetIntakesForDate;
using NutritionTracker.Application.Queries.SearchForFood;
using System.Security.Claims;

namespace NutritionTracker.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class SearchController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;

		[HttpGet]
		public async Task<IActionResult> SearchForFood([FromQuery] string query)
		{
			try
			{
				var searchQuery = new SearchForFoodQuery
				{
					Query = query
				};

				var result = await _mediator.Send(searchQuery);

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
