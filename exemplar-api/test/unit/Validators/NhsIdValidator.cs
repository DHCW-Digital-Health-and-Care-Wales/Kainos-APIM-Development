using DHCW.PD.Validators;

namespace UnitTests.Validators;

public class NhsIdValidatorTest
{

    NhsIdValidator _validator;

    public NhsIdValidatorTest()
    {
        _validator = new NhsIdValidator();
    }

    [Fact]
    public void Validate_NhsId_Invalid()
    {
        // Arrange
        var nhsId = "4857773456";
        // Act
        var result = _validator.IsValid(nhsId);
        // Assert
        Assert.True(!result);
    }

    [Fact]
    public void Validate_NhsId_Valid()
    {
        // Arrange
        var nhsId = "4857773457";
        // Act
        var result = _validator.IsValid(nhsId);
        // Assert
        Assert.True(result);
    }
}
