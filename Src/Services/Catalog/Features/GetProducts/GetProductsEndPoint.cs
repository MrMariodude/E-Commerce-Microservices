namespace Catalog.API.Features.GetProducts;

public record GetProductsItemResponse(
    Guid ID,
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price
);

public record GetProductsResponse(
    IReadOnlyList<GetProductsItemResponse> Items,
    long Count,
    bool IsFirst,
    bool IsLast,
    long PageNumber,
    long PageSize,
    long TotalItemCount,
    long PageCount
);

public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/products",
                async (ISender sender, int pageNumber = 1, int pageSize = 10) =>
                {
                    GetProductsResult result = await sender.Send(
                        new GetProductsQuery(pageNumber, pageSize)
                    );
                    GetProductsResponse response = result.Adapt<GetProductsResponse>();
                    return Results.Ok(response);
                }
            )
            .WithName("GetProducts")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
    }
}
