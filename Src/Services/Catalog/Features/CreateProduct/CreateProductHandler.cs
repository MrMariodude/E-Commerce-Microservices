namespace Catalog.API.Features.CreateProduct;

//? This is not a command this is a http payload disguised as a command
public record CreateProductCommand(
    string Name,
    List<string> Categories, //? what are these names? slugs? ids? domain smell 🚨
    string Description,
    string ImageFile, //? 1) This should be handled by UI Frontend not us backend and as a string? how not even a multipart to store it in a storage like MinIO
    //? 2) does the product need an image at the creation time?

    decimal Price //? are we gonna depend on the front for the price validation? currency & rules?
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid ID);

//? 1) This validations are so shallow they are only validate the shape of the http request in real application this validators can become more complex
//? 2) This is input validation, not domain validation we need to enfore the busuiness rules inside domain models not only in the validators if we do so our system will be come unsafe,
//? ... every single entry point in our system provide a different way to enfore the domain rules
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    //*FluentValidation should:
    //*Guard against garbage input -> rules that define the request
    //*Protect the handler from obvious nonsense -> do we have all needed data or are we missing something?
    //*Fail fast at API boundary -> terminate the request earlly in case of error
    //* It is NOT:
    //*domain invariant enforcement
    //*business policy enforcement
    //*consistency guarantee
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Categories).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

//? We injected the IDocumentSession direclty here because it's already an abstraction for the database operations so we dont need to add
//? another useless abstract layer
internal class CreateProductHandler(IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken
    )
    {
        //! 1) create project entity for domain model
        var product = new Product
        {
            Name = command.Name,
            Categories = command.Categories,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
        };

        //! 2) send to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        //! 3) return id of the created product
        return new CreateProductResult(product.ID);
    }
}
