namespace Basket.Api.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasketByUserName(
        string userName,
        CancellationToken cancellationToken = default
    );
    Task<ShoppingCart> StoreBasket(
        ShoppingCart cart,
        CancellationToken cancellationToken = default
    );
    Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
}
