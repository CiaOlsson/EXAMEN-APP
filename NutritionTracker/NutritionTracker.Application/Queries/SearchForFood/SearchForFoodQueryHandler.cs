using Mediator;
using NutritionTracker.Application.Interfaces;
using NutritionTracker.Application.Queries.GetIntakesForDate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTracker.Application.Queries.SearchForFood
{
	public class SearchForFoodQueryHandler : IRequestHandler<SearchForFoodQuery, List<FoodDTO>>
	{
		private readonly IFoodRepository _foodRepo;

		public SearchForFoodQueryHandler(IFoodRepository foodRepo)
		{
			_foodRepo = foodRepo;
		}
		public async ValueTask<List<FoodDTO>> Handle(SearchForFoodQuery query, CancellationToken cancellationToken)
		{
			var searchResult = await _foodRepo.SearchByNameAsync(query.Query);

			var mappedFood = new List<FoodDTO>();

			foreach (var food in searchResult)
			{
				mappedFood.Add(new FoodDTO
				{
					FoodId = food.FoodId,
					Name = food.Name,
					FoodGroup = food.Group,
					Energy_kcal = food.Energy_kcal,
				});
			}

			return mappedFood;
		}
	}
}
