namespace Basket.Models;

public class ShoppingCart
{
    public string UserName { get; set; } = default!;
    public List<ShppingCartItem> Items { get; set; } = default!;
    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);

    public ShoppingCart(string UserName)
    {
        this.UserName = UserName;
    }

    public ShoppingCart() { }
}
