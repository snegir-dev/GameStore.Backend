using System.Reflection;
using GameStore.Application;
using GameStore.Application.Common.Mappings;
using GameStore.Application.Interfaces;
using GameStore.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IGameStoreDbContext).Assembly));
});
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<GameStoreDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();