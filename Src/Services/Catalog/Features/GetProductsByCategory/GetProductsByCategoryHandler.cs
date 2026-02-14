namespace Catalog.API.Features.GetProductsByCategory;

public record GetProductsByCategoryQuery(string Categories) : IQuery<GetProductsByCategoryResult>;

public record GetProductsByCategoryResult(IEnumerable<Product> Products);

internal class GetProductsByCategoryHandler(IDocumentSession session)
    : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(
        GetProductsByCategoryQuery query,
        CancellationToken cancellationToken
    )
    {
        //! 1) search by categories client provide

        //? this is not ideal and can cause hard we have to enforce a strongly typed categories to prevent trash data and terminate the request early in case of mistake from client side
        //* by creating hashset for the list we reduce the complexity from O(N*M) to O(N)
        HashSet<string> categories = [.. query.Categories.Split(",")];
        var result = await session
            .Query<Product>()
            .Where(product => product.Categories.Any(category => categories.Contains(category)))
            .ToListAsync();

        return new GetProductsByCategoryResult(result);
    }
}
