using Mediator;
using NutritionTracker.Application.Interfaces;
using NutritionTracker.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NutritionTracker.Application.Queries.GetIntakeForToday
{
	public class GetIntakeForTodayQueryHandler : IRequestHandler<GetIntakeForTodayQuery, List<GetIntakeForTodayDTO>>
	{
		private readonly IEventStore _eventStore;

		public GetIntakeForTodayQueryHandler(IEventStore eventStore)
		{
			_eventStore = eventStore;
		}

		public async ValueTask<List<GetIntakeForTodayDTO>> Handle(GetIntakeForTodayQuery query, CancellationToken cancellationToken)
		{
			var userEvents = await _eventStore.GetEventsAsync(query.UserId);
			var today = DateTime.UtcNow.Date;

			var intakeAddedEventsToday = userEvents.Where(e => e.EventType == "IntakeAddedEvent" && e.Timestamp.Date == today).ToList();
			//Om jag lägger till att man får ta bort tillagd mat så måste jag också välja ut dessa event, jämföra eventets id? deleted vs added.... 

			var intakesToday = new List<GetIntakeForTodayDTO>();

			foreach ( var intake in intakeAddedEventsToday)
			{
				var intakeData = JsonSerializer.Deserialize<IntakeAddedEvent>(intake.Data);

				if (intakeData is null)
					continue;

				var getIntakeForTodayDTO = new GetIntakeForTodayDTO
				{
					FoodId = intakeData.FoodId,
					Name = intakeData.Name,
					Calories = intakeData.Calories,
					Protein = intakeData.Protein,
					Fat = intakeData.Fat,
					Carbohydrates = intakeData.Carbohydrates
				}; 

				intakesToday.Add(getIntakeForTodayDTO);
			}

			return intakesToday;
		}
	}
}
