﻿using Mediator;
using NutritionTracker.Application.Interfaces;
using NutritionTracker.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NutritionTracker.Application.Queries.GetIntakesForDate
{
	public class GetIntakesForDateQueryHandler : IRequestHandler<GetIntakesForDateQuery, List<IntakeDTO>>
	{
		private readonly IEventStore _eventStore;

		public GetIntakesForDateQueryHandler(IEventStore eventStore)
		{
			_eventStore = eventStore;
		}

		public async ValueTask<List<IntakeDTO>> Handle(GetIntakesForDateQuery query, CancellationToken cancellationToken)
		{
			var userEvents = await _eventStore.GetEventsAsync(query.UserId); 

			var intakeAddedEvents = userEvents.Where(e => e.EventType == "IntakeAddedEvent" ).ToList();
			//Om jag lägger till att man får ta bort tillagd mat så måste jag också välja ut dessa event, jämföra eventets id? deleted vs added.... 

			var intakesToday = new List<IntakeDTO>();

			foreach ( var intake in intakeAddedEvents) //Detta är ju typ redan som en check att se om det finns några event i listan. 
			{
				var intakeData = JsonSerializer.Deserialize<IntakeAddedEvent>(intake.Data);

				if (intakeData is null)
					continue;

				//Här efter serialiseringen kan jag kolla så att eventet har rätt datum. 
				if (intakeData.DateOfIntake == query.DateOfIntake)
				{
					var getIntakeForTodayDTO = new IntakeDTO
					{
						FoodId = intakeData.FoodId,
						Name = intakeData.Name,
						Energy_kcal = intakeData.Energy_kcal,
						Protein = intakeData.Protein,
						Fat = intakeData.Fat,
						Carbohydrates = intakeData.Carbohydrates,
						Fiber = intakeData.Fiber,
						SugarsTotal = intakeData.SugarsTotal,
						Salt = intakeData.Salt,
						Vitamin_A = intakeData.Vitamin_A,
						Vitamin_B6 = intakeData.Vitamin_B6,
						Vitamin_B12 = intakeData.Vitamin_B12,
						Vitamin_C = intakeData.Vitamin_C,
						Vitamin_D = intakeData.Vitamin_D,
						Vitamin_E = intakeData.Vitamin_E,
						Vitamin_K = intakeData.Vitamin_K
					};

					intakesToday.Add(getIntakeForTodayDTO);
				}
			}

			return intakesToday;
		}
	}
}
