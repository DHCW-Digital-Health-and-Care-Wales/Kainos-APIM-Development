using System.Diagnostics.CodeAnalysis;

namespace DHCW.PD.Exceptions;

[ExcludeFromCodeCoverage]
public sealed class NotFoundException : Exception
{
    private const string ExceptionMessage = "Not found";

    public NotFoundException() : base(ExceptionMessage) { }
    public NotFoundException(Exception cause) : base(ExceptionMessage, cause) { }
}