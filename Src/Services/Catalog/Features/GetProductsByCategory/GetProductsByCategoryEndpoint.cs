namespace Catalog.API.Features.GetProductsByCategory;

public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

//? This is wrong we have to create a dedicated paginated endpoint with all grouping filters not for every single filter to avoid endpoints exploding!
public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/products/category/{categories}",
                async (string categories, ISender sender) =>
                {
                    GetProductsByCategoryResult result = await sender.Send(
                        new GetProductsByCategoryQuery(categories)
                    );
                    GetProductsByCategoryResponse productResponse =
                        result.Adapt<GetProductsByCategoryResponse>();
                    return productResponse;
                }
            )
            .WithName("GetProductsByCategory")
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products By Category")
            .WithDescription("Get Products By Category");
    }
}
