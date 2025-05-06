using Microsoft.EntityFrameworkCore;
using NutritionTracker.Application.Interfaces;
using NutritionTracker.Application.Services;
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
			.OrderBy(e=> e.Timestamp)// då får man alla event för användaren sedan någon annan stans ska man kolla vilka typer av event man vill sortera ut osv. detta ska nog typ hanteras av projektionen??
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
