using NutritionTracker.Application.Interfaces;
using NutritionTracker.Domain.DomainEntities;
using System.Text.Json;

namespace NutritionTracker.Infrastructure.Factory
{
	public class DomainEventFactory : IDomainEventFactory
	{
		public DomainEvent Create<T>(T @event, Guid userId, int version = 1)
		{
			return new DomainEvent
			{
				Id = Guid.NewGuid(),
				UserId = userId,
				Version = version,
				Timestamp = DateTime.UtcNow,
				EventType = typeof(T).Name,
				Data = JsonSerializer.Serialize(@event)
			};
		}
	}
}
