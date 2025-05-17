using Microsoft.EntityFrameworkCore;
using NutritionTracker.Application.Interfaces;
using NutritionTracker.Infrastructure.Factory;
using NutritionTracker.Domain.DomainEntities;
using NutritionTracker.Infrastructure.DbContext;
using System;


namespace NutritionTracker.Infrastructure.Repositories
{
	public class EventStore(NutritionTrackerDbContext context, IDomainEventFactory factory) : IEventStore
	{
		private readonly NutritionTrackerDbContext _context = context;
		private readonly IDomainEventFactory _factory = factory;

		public async Task<List<DomainEvent>> GetEventsAsync(Guid userId)
		{
			var result = await _context.Events
			.Where(e => e.UserId == userId)
			.OrderBy(e=> e.Timestamp)
			.ToListAsync();

			return result;
		}

		public async Task SaveEventAsync<TEvent>(TEvent @event, Guid userId)
		{
			var userEvents = await GetEventsAsync(userId);
			var version = userEvents.Count();
			version++;

			var domainEvent = _factory.Create(@event, userId, version);
			await _context.Events.AddAsync(domainEvent);
			await _context.SaveChangesAsync();
		}
	}
}
