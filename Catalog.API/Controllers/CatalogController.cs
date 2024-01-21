using Catalog.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[Route("/v1/catalog")]

public class CatalogController : ControllerBase
{
    private readonly ITheMealService _theMealService;
    public CatalogController(ITheMealService theMealService)
    {
        _theMealService = theMealService;
    }

    [HttpGet("get-meals/country")]
    public async Task<IActionResult> GetMealByCountry(string country)
    {
        return Ok(await _theMealService.FetchMealDataAsync(country));

    }

    [HttpGet("get-categories")]
    public async Task<IActionResult> GetMealCategories()
    {
        return Ok(await _theMealService.FetchMealCategoriesAsync());

    }

    [HttpGet("get-meal-detail/id")]
    public async Task<IActionResult> GetMealDetails(string Id)
    {
        return Ok(await _theMealService.FetchMealDetailByIdAsync(Id));

    }


    [HttpGet("search-by-name/name")]
    public async Task<IActionResult> SearchByName(string Name)
    {
        return Ok(await _theMealService.SearchMealByNameAsync(Name));

    }


    [HttpGet("search-by-category/category")]
    public async Task<IActionResult> SearchByCategory(string Category)
    {
        return Ok(await _theMealService.SearchMealCategoryAsync(Category));

    }
}
