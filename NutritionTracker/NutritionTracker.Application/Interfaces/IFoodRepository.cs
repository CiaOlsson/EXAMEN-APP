using NutritionTracker.Domain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTracker.Application.Interfaces
{
	public interface IFoodRepository
	{
		Task<FoodEntity> GetFoodById(int foodId);
	}
}
