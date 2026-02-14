namespace Catalog.API.Features.GetProducts;

public record GetProductsResponse(
    Guid ID,
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price
);

public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/products",
                async (ISender sender) =>
                {
                    GetProductsResult result = await sender.Send(new GetProductsQuery());
                    var resultDto = result.Products.Adapt<List<GetProductsResponse>>();
                    return Results.Ok(resultDto);
                }
            )
            .WithName("GetProducts")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
    }
}
