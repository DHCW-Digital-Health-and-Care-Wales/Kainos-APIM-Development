using System.Diagnostics.CodeAnalysis;

namespace DHCW.PD.Exceptions;

[ExcludeFromCodeCoverage]
public sealed class UnauthorizedException : Exception
{
    private const string ExceptionMessage = "Unauthorized";

    public UnauthorizedException() : base(ExceptionMessage) { }
    public UnauthorizedException(Exception cause) : base(ExceptionMessage, cause) { }
}