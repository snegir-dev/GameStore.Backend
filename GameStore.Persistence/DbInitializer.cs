namespace GameStore.Persistence;

public class DbInitializer
{
    public static void Initialize(GameStoreDbContext context)
    {
        context.Database.EnsureCreated();
    }
}