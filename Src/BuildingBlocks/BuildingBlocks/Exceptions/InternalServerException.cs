namespace BuildingBlocks.Exceptions;

public class InternalServerException : Exception
{
    public string? Details { get; set; }

    public InternalServerException(string message)
        : base(message) { }

    public InternalServerException(string message, string Details)
        : base(message)
    {
        this.Details = Details;
    }
}
