namespace BuildingBlocks.Exceptions;

public class BadRequestException : Exception
{
    public string? Details { get; set; }

    public BadRequestException(string message)
        : base(message) { }

    public BadRequestException(string message, string Details)
        : base(message)
    {
        this.Details = Details;
    }
}
