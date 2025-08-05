namespace DHCW.PD.Exceptions;

public class BadRequestException : Exception
{
    private const string ExceptionMessage = "Bad request";

    public BadRequestException() : base(ExceptionMessage) { }
    public BadRequestException(Exception cause) : base(ExceptionMessage, cause) { }
}