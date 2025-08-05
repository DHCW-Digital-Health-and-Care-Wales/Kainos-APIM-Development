namespace DHCW.PD.Validators;

[ExcludeFromCodeCoverage]
public interface IValidator<DataType, ReturnType>
{
    public ReturnType IsValid(DataType data);
}
