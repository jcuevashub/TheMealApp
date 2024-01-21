namespace Catalog.Application.DTOs;

public class MealDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string MealThumb { get; set; }
    public string? Category { get; set; }
    public string? Instructions { get; set; }
    public string? Area { get; set; }

}
