namespace DHCW.PD.Exceptions;

public class TimeoutException : Exception
{
    private const string ExceptionMessage = "Timeout";

    public TimeoutException() : base(ExceptionMessage) { }
    public TimeoutException(Exception cause) : base(ExceptionMessage, cause) { }
}