namespace DHCW.PD.Validators;

public interface IValidator<DataType, ReturnType>
{
    public ReturnType IsValid(DataType data);
}
