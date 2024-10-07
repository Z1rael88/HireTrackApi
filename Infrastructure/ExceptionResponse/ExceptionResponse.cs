namespace Infrastructure.ExceptionResponse
{
public class ExceptionResponse
{
    public required string Message { get; set; }

    public required ICollection<string> Errors { get; set; }
}
}