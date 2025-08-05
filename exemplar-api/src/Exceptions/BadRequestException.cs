using System.Diagnostics.CodeAnalysis;

namespace DHCW.PD.Exceptions;

[ExcludeFromCodeCoverage]
public class BadRequestException : Exception
{
    private const string ExceptionMessage = "Bad request";

    public BadRequestException() : base(ExceptionMessage) { }
    public BadRequestException(Exception cause) : base(ExceptionMessage, cause) { }
}