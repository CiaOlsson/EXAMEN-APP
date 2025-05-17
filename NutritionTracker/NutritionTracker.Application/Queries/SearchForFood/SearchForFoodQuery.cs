using Mediator;
using NutritionTracker.Domain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTracker.Application.Queries.SearchForFood
{
	public class SearchForFoodQuery: IRequest<List<FoodDTO>>
	{
		public string Query {  get; set; }
	}
}
