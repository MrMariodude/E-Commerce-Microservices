using Basket.Api.Data;

namespace Basket.Api.Features.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart ShoppingCart);

public class GetBasketQueryHandler(IBasketRepository repository)
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(
        GetBasketQuery request,
        CancellationToken cancellationToken
    )
    {
        var basket = await repository.GetBasketByUserName(request.UserName, cancellationToken);

        return new GetBasketResult(basket);
    }
}
