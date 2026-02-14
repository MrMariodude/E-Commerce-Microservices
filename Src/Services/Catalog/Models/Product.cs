namespace Catalog.API.Models;

//? Product 1-has->Many Qategories
public class Product
{
    public Guid ID { get; set; }
    public string Name { get; set; } = default!;
    public List<string> Categories { get; set; } = []; //? this is a questionable as what we mean by categories

    //*
    //? In e-commerce, Category is its own concept (often its own aggregate). It shouldn’t be “owned” by Product.
    //? In real application we should define what "Category" actually means is it a complex object with different things like(slug,description,path,etc..) or a simple name like now
    //? Right now this is a bad business domain model for products to include categories inside it but we will leave it for the sake of the course
    // */
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; }
}
