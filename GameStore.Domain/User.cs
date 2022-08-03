using Microsoft.AspNetCore.Identity;

namespace GameStore.Domain;

public class User : IdentityUser<long>
{
    public decimal Balance { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    
    public List<Game> Games { get; set; }
    public List<Basket> Baskets { get; set; }
}