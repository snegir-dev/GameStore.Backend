namespace GameStore.Domain;

public class Basket
{
    public long Id { get; set; }
    
    public User User { get; set; }
    public List<Game> Games { get; set; }
}