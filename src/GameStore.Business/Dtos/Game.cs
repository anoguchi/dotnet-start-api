namespace GameStore.Business.Dtos;

public class Game : Entity
{
    public string? Name { get; set; }
    public string? Genre { get; set; }
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
}