namespace GameStore.Domain;

public class Game
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateOnly DateRelease { get; set; }
    public decimal Price { get; set; }
    
    public Company Company { get; set; }
    public Publisher Publisher { get; set; }
    public List<Basket> Baskets { get; set; }
    public List<Genre> Genres { get; set; }
    public List<User> Users { get; set; }
}