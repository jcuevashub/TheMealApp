using Catalog.Application.DTOs;

namespace Catalog.Application.Interfaces.Services;

public interface ITheMealService
{
    Task<MealDto> FetchMealDataAsync(string country);

}
