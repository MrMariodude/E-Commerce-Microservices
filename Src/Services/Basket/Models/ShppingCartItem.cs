namespace Basket.Models;

public class ShppingCartItem
{
    public int Quantity { get; set; } = default!;
    public string Color { get; set; } = default!;
    public decimal Price { get; set; } = default!;
    public Guid ProductID { get; set; } = default!;
    public string ProductName { get; set; } = default!;
}
