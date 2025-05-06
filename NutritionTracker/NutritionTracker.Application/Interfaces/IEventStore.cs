using NutritionTracker.Domain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTracker.Application.Interfaces
{
	public interface IEventStore
	{
		Task SaveEventAsync<TEvent>(TEvent @event, Guid userId);
		Task<List<DomainEvent>> GetEventsAsync(Guid UserId);
	}
}
