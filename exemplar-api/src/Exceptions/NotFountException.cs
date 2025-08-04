namespace DHCW.PD.Exceptions;

public class NotFoundException : Exception
{
    private const string ExceptionMessage = "Not found";

	public NotFoundException() : base(ExceptionMessage) {}
	public NotFoundException(Exception cause) : base(ExceptionMessage, cause) {}
}