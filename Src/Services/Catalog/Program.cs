using BuildingBlocks.Exceptions.Handlers;

namespace Catalog.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //? DI services adding.
        //? 1) Carter need to be add for each microservices as it does not contain a method for read from asseembly
        builder.Services.AddCarter();
        //? 2) register Concordia handlers via reflection (MediatR-style).
        builder.Services.AddMediator(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        builder
            //? 3) convert the normal postgres into documentDB like mongo and use the type lightweightsessiong instead of the other 2,
            //? (identity map document -> return from db the same object refrence
            //? , dirtycheckingdocument -> can store logic changes without need to session.save()),
            //?  as it optimized for, read and write operations
            .Services.AddMarten(options =>
            {
                options.Connection(builder.Configuration.GetConnectionString("Database")!);
            })
            .UseLightweightSessions();
        //? 4) Register openApi for scalar docs usage
        builder.Services.AddOpenApi();
        //? 5) Add fluent validation
        builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        //? 6) Add global exception handler
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        var app = builder.Build();
        //? Request/response pipeline middlewares.
        //? 1) enforce secure redicretion
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
