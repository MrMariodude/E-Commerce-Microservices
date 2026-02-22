using Marten.Pagination;

namespace Catalog.API.Features.GetProducts;

public record GetProductsQuery(int PageNumber = 1, int PageSize = 10) : IQuery<GetProductsResult>;

public record GetProductsResult(
    IReadOnlyList<Product> Items,
    long Count,
    bool IsFirst,
    bool IsLast,
    long PageNumber,
    long PageSize,
    long TotalItemCount,
    long PageCount
);

internal class GetProductsHandler(IDocumentSession session, ILogger<GetProductsHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(
        GetProductsQuery query,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("🔄️ Starting get products from database");
        var products = await session
            .Query<Product>()
            .ToPagedListAsync(query.PageNumber, query.PageSize, cancellationToken);

        return new GetProductsResult(
            products.ToList(),
            products.Count,
            products.IsFirstPage,
            products.IsLastPage,
            products.PageNumber,
            products.PageSize,
            products.TotalItemCount,
            products.PageCount
        );
    }
}
