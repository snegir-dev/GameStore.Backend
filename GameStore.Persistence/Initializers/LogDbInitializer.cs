using GameStore.Persistence.Contexts;

namespace GameStore.Persistence.Initializers;

public static class LogDbInitializer
{
    public static void Initializer(LogDbContext context)
    {
        context.Database.EnsureCreated();
    }
}