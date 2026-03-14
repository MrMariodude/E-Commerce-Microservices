using Scalar.AspNetCore;
using Basket.Api.Data;

namespace Basket.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddCarter();

        builder.Services.AddMediator(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        builder
            .Services.AddMarten(options =>
            {
                options.Connection(builder.Configuration.GetConnectionString("Database")!);
                options.Schema.For<ShoppingCart>().Identity(cart => cart.UserName);
            })
            .UseLightweightSessions();
        builder.Services.AddScoped<IBasketRepository, BasketRepository>();
        builder.Services.AddOpenApi();
        builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        var app = builder.Build();
        app.UseHttpsRedirection();
        //? 2) scalar
        app.MapOpenApi();
        app.MapScalarApiReference("/docs", options => options.WithTheme(ScalarTheme.Default));
        //? 3) shows carter endpoints for scalar
        app.MapCarter();
        //? 4) Catch the exceeption during return and handle it properly
        app.UseExceptionHandler(option => { });
        app.MapGet("/", () => Results.Redirect("/docs")).ExcludeFromDescription();
        app.Run();
    }
}
