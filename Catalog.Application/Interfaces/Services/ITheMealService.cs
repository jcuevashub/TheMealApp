using Catalog.Application.DTOs;

namespace Catalog.Application.Interfaces.Services;

public interface ITheMealService
{
    Task<List<MealDto>> SearchMealByNameAsync(string Name);
    Task<List<MealDto>> SearchMealCategoryAsync(string Category);
    Task<List<MealDto>> FetchMealDataAsync(string Country);
    Task<List<MealCategoryDto>> FetchMealCategoriesAsync();
    Task<MealDto> FetchMealDetailByIdAsync(string Id);


}
