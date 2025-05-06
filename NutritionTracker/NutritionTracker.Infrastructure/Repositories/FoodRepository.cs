using Microsoft.Identity.Client;
using NutritionTracker.Application.Interfaces;
using NutritionTracker.Domain.DomainEntities;
using NutritionTracker.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTracker.Infrastructure.Repositories
{
	public class FoodRepository(NutritionTrackerDbContext context) : IFoodRepository
	{
		private readonly NutritionTrackerDbContext _context = context;

		public async Task<FoodEntity> GetFoodById(int foodId)
		{
			var foodItem = _context.Foods.SingleOrDefault(food => food.FoodId == foodId);

			return foodItem;
		}
	}
}
