using Microsoft.Identity.Client;
using NutritionTracker.Application.Interfaces;
using NutritionTracker.Domain.DomainEntities;
using NutritionTracker.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NutritionTracker.Infrastructure.Repositories
{
	public class FoodRepository(NutritionTrackerDbContext context) : IFoodRepository
	{
		private readonly NutritionTrackerDbContext _context = context;

		public async Task<FoodEntity> GetFoodById(int foodId)
		{
			var foodItem = await _context.Foods.SingleOrDefaultAsync(food => food.FoodId == foodId);

			return foodItem;
		}

		public async Task<List<FoodEntity>> SearchByNameAsync(string query)
		{
			return await _context.Foods
				.Where(f => f.Name.Contains(query))
				.Take(100)
				.ToListAsync();
		}
	}
}
