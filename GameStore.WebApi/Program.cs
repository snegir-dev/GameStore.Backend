using System.Reflection;
using GameStore.Application;
using GameStore.Application.Common.Converters;
using GameStore.Application.Common.Mappings;
using GameStore.Application.Interfaces;
using GameStore.Domain;
using GameStore.Persistence;
using GameStore.Persistence.Contexts;
using GameStore.Persistence.Initializers;
using GameStore.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromFile("NLog.config", false)
    .GetCurrentClassLogger();
logger.Debug("Init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers()
        .AddNewtonsoftJson();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAutoMapper(config =>
    {
        config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        config.AddProfile(new AssemblyMappingProfile(typeof(IGameStoreDbContext).Assembly));
    });

    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddApplication(builder.Configuration);

    builder.Services.AddIdentity<User, IdentityRole<long>>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 5;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
        })
        .AddEntityFrameworkStores<GameStoreDbContext>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
    });

    builder.Services.AddControllers(options =>
        {
            options.CacheProfiles.Add("Caching", 
                new CacheProfile()
                {
                    Duration = 300,
                    Location = ResponseCacheLocation.Any
                });
            options.CacheProfiles.Add("NoCaching", 
                new CacheProfile()
                {
                    Location = ResponseCacheLocation.None,
                    NoStore = true
                });
        })
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Converters.Add(new DateOnlyJsonConverter());
        });

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    });
    
    builder.Services.AddResponseCaching();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var serviceProvider = scope.ServiceProvider;
        try
        {
            var context = serviceProvider.GetRequiredService<GameStoreDbContext>();
            DbInitializer.Initialize(context);
            
            var rolesManager = scope.ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole<long>>>();
            var userManager = scope.ServiceProvider
                .GetRequiredService<UserManager<User>>();
            await RoleInitializer.InitializerAsync(rolesManager, userManager);
            
            var contextLog = serviceProvider.GetRequiredService<LogDbContext>();
            LogDbInitializer.Initializer(contextLog);
        }
        catch (Exception e)
        {
            logger.Error(e, "Error - {E}", e);
            throw;
        }
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCustomExceptionHandler();

    app.UseHttpsRedirection();

    app.UseCors("AllowAll");
    
    app.UseResponseCaching();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    logger.Error(e, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}