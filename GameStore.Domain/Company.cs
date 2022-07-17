namespace GameStore.Domain;

public class Company
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public List<Game> Games { get; set; }
}