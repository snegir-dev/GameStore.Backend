namespace GameStore.Domain;

public class Publisher
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public List<Game> Games { get; set; }
}