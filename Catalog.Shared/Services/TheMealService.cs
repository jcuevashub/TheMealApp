using Catalog.Application.DTOs;
using Catalog.Application.Interfaces.Services;
using Newtonsoft.Json;

namespace Catalog.Shared.Services;

public class TheMealService : ITheMealService
{
    private readonly HttpClient _httpClient;

    public TheMealService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MealCategoryDto>> FetchMealCategoriesAsync()
    {
        string apiUrl = $"https://www.themealdb.com/api/json/v1/1/categories.php";

        try
        {
            var responseString = await _httpClient.GetStringAsync(apiUrl);

            var responseData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(responseString);
            var categories = new List<MealCategoryDto>();

            if (responseData != null && responseData.ContainsKey("categories"))
            {
                if (responseData["categories"] == null) return categories;

                foreach (var meal in responseData["categories"])
                {
                    categories.Add(new MealCategoryDto
                    {
                        Id = meal["idCategory"],
                        Name = meal["strCategory"],
                        CategoryThumb = meal["strCategoryThumb"],
                        Description = meal["strCategoryDescription"]
                    });
                }
            }

            return categories;

        }
        catch (Exception e)
        {

            throw new Exception($"Error fetching meals data: {e}");

        }
    }

    public async Task<List<MealDto>> FetchMealDataAsync()
    {
        string apiUrl = $"https://www.themealdb.com/api/json/v1/1/random.php";

        try
        {
            var responseString = await _httpClient.GetStringAsync(apiUrl);

            var responseData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(responseString);
            var meals = new List<MealDto>();

            if (responseData != null && responseData.ContainsKey("meals"))
            {
                if (responseData["meals"] == null) return meals;

                foreach (var meal in responseData["meals"])
                {
                    meals.Add(new MealDto
                    {
                        Id = meal["idMeal"],
                        Name = meal["strMeal"],
                        MealThumb = meal["strMealThumb"],
                        Category = meal["strCategory"],
                        Area = meal["strArea"],
                        Instructions = meal["strInstructions"]
                    });
                }
            }

            return meals;

        }
        catch (Exception e)
        {

            throw new Exception($"Error fetching meals data: {e}");

        }

    }

    public async Task<MealDto> FetchMealDetailByIdAsync(string Id)
    {
        string apiUrl = $"https://www.themealdb.com/api/json/v1/1/lookup.php?i={Id}";

        try
        {
            var responseString = await _httpClient.GetStringAsync(apiUrl);

            var responseData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(responseString);

            if (responseData == null && !responseData.ContainsKey("meals")) return null;
            var meal = responseData["meals"];

            return new MealDto
            {
                Id = meal[0]["idMeal"],
                Name = meal[0]["strMeal"],
                MealThumb = meal[0]["strMealThumb"],
                Category = meal[0]["strCategory"],
                Area = meal[0]["strArea"],
                Instructions = meal[0]["strInstructions"]
            };
        }
        catch (Exception e)
        {

            throw new Exception($"Error fetching meals data: {e}");

        }
    }

    public async Task<List<MealDto>> SearchMealByNameAsync(string Name)
    {
        string apiUrl = $"https://www.themealdb.com/api/json/v1/1/search.php?s={Name}";

        try
        {
            var responseString = await _httpClient.GetStringAsync(apiUrl);

            var responseData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(responseString);
            var meals = new List<MealDto>();

            if (responseData != null && responseData.ContainsKey("meals"))
            {
                if (responseData["meals"] == null) return meals;

                foreach (var meal in responseData["meals"])
                {

                    meals.Add(new MealDto
                    {
                        Id = meal["idMeal"],
                        Name = meal["strMeal"],
                        MealThumb = meal["strMealThumb"],
                        Category = meal["strCategory"],
                        Area = meal["strArea"],
                        Instructions = meal["strInstructions"]
                    });
                }
            }

            return meals;

        }
        catch (Exception e)
        {

            throw new Exception($"Error fetching meals data: {e}");

        }

    }

    public async Task<List<MealDto>> SearchMealCategoryAsync(string Category)
    {
        string apiUrl = $"https://www.themealdb.com/api/json/v1/1/filter.php?c={Category}";

        try
        {
            var responseString = await _httpClient.GetStringAsync(apiUrl);

            var responseData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(responseString);
            var meals = new List<MealDto>();

            if (responseData != null && responseData.ContainsKey("meals"))
            {
                if (responseData["meals"] == null) return meals;

                foreach (var meal in responseData["meals"])
                {
                    meals.Add(new MealDto
                    {
                        Id = meal["idMeal"],
                        Name = meal["strMeal"],
                        MealThumb = meal["strMealThumb"],
                        Category = meal["strCategory"],
                        Area = meal["strArea"],
                        Instructions = meal["strInstructions"]
                    });
                }
            }

            return meals;

        }
        catch (Exception e)
        {

            throw new Exception($"Error fetching meals data: {e}");

        }

    }
}
