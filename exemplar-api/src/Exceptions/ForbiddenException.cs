using System.Diagnostics.CodeAnalysis;

namespace DHCW.PD.Exceptions;

[ExcludeFromCodeCoverage]
public sealed class ForbiddenException : Exception
{
    private const string ExceptionMessage = "Forbidden";

    public ForbiddenException() : base(ExceptionMessage) { }
    public ForbiddenException(Exception cause) : base(ExceptionMessage, cause) { }
}