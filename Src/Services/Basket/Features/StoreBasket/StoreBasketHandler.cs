using Basket.Api.Data;

namespace Basket.Api.Features.StoreBasket;

public record StoreBasketCommand(string UserName, List<StoreBasketItemCommand> Items)
    : ICommand<StoreBasketRessult>;

public record StoreBasketItemCommand(
    int Quantity,
    string Color,
    decimal Price,
    Guid ProductId,
    string ProductName
);

public record StoreBasketRessult(string UserName);

public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
        RuleFor(x => x.Items).NotEmpty().WithMessage("Items can not be empty");
        RuleForEach(x => x.Items)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required");
                item.RuleFor(x => x.ProductName).NotEmpty().WithMessage("ProductName is required");
                item.RuleFor(x => x.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than 0");
                item.RuleFor(x => x.Price)
                    .GreaterThan(0)
                    .WithMessage("Price must be greater than 0");
            });
    }
}

public class StoreBasketCommandHandler(IBasketRepository repository)
    : ICommandHandler<StoreBasketCommand, StoreBasketRessult>
{
    public async Task<StoreBasketRessult> Handle(
        StoreBasketCommand command,
        CancellationToken cancellationToken
    )
    {
        var cart = new ShoppingCart(command.UserName)
        {
            Items = command
                .Items.Select(item => new ShppingCartItem
                {
                    Quantity = item.Quantity,
                    Color = item.Color,
                    Price = item.Price,
                    ProductID = item.ProductId,
                    ProductName = item.ProductName,
                })
                .ToList(),
        };

        var basket = await repository.StoreBasket(cart, cancellationToken);

        return new StoreBasketRessult(basket.UserName);
    }
}
