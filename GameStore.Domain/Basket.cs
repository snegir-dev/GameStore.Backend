namespace GameStore.Domain;

public class Basket
{
    public long Id { get; set; }
    
    public User User { get; set; }
    public Game Game { get; set; }
}