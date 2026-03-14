using BuildingBlocks.Exceptions;

namespace Basket.Api.Exceptions;

public class BasketNotFoundException(string userName)
    : NotFoundException("ShoppingCart", userName) { }
