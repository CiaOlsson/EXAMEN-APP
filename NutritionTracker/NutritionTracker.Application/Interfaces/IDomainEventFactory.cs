using NutritionTracker.Domain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTracker.Application.Interfaces
{
	public interface IDomainEventFactory
	{
		DomainEvent Create<T>(T @event, Guid userId, int version = 1);

	}
}
