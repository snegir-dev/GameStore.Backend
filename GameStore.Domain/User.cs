using Microsoft.AspNetCore.Identity;

namespace GameStore.Domain;

public class User : IdentityUser<long>
{
    public decimal Balance { get; set; }
    public List<Game> Games { get; set; }
}