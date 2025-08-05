namespace DHCW.PD.Validators;

public interface INhsIdValidator : IValidator<string, bool> { }

public class NhsIdValidator : INhsIdValidator
{
    public bool IsValid(string value)
    {
        if (value.Length != 10 || !value.All(char.IsDigit))
            return false;

        int total = 0;

        for (int i = 0; i < 9; i++)
        {
            int digit = value[i] - '0';
            int weight = 10 - i;
            total += digit * weight;
        }

        int remainder = total % 11;
        int checkDigit = 11 - remainder;

        if (checkDigit == 11)
            checkDigit = 0;
        else if (checkDigit == 10)
            return false;

        int actualCheckDigit = value[9] - '0';
        return checkDigit == actualCheckDigit;
    }
}
