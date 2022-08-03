using GameStore.Persistence.Contexts;

namespace GameStore.Persistence.Initializers;

public class DbInitializer
{
    public static void Initialize(GameStoreDbContext context)
    {
        context.Database.EnsureCreated();
    }
}