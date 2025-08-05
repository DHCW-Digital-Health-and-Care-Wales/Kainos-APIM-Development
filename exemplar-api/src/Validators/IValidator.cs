namespace DHCW.PD.Validators;
using System.Diagnostics.CodeAnalysis;

public interface IValidator<DataType, ReturnType>
{
    public ReturnType IsValid(DataType data);
}
