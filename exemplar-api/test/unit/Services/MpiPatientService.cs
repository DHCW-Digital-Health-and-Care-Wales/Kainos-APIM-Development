using Xunit;
using Moq;
using Hl7.Fhir.Model;
using DHCW.PD.Services;
using DHCW.PD.Validators;
using DHCW.PD.Helpers;
using DHCW.PD.Exceptions;
using Microsoft.Extensions.Logging;
using System.IO;

public class MpiPatientServiceTests
{
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<INhsIdValidator> _validatorMock;
    private readonly Mock<ILogger<MpiPatientService>> _loggerMock;
    private readonly PatientBuilder _patientBuilder;
    private readonly MpiPatientService _service;

    public MpiPatientServiceTests()
    {
        _configurationMock = new Mock<IConfiguration>();
        _validatorMock = new Mock<INhsIdValidator>();
        _loggerMock = new Mock<ILogger<MpiPatientService>>();
        _patientBuilder = new PatientBuilder();

        _service = new MpiPatientService(
            _configurationMock.Object,
            _validatorMock.Object,
            _patientBuilder,
            _loggerMock.Object
        );
    }

    [Theory]
    [InlineData("1111142799", typeof(BadRequestException))]
    [InlineData("2222242799", typeof(UnauthorizedException))]
    [InlineData("3333342799", typeof(ForbiddenException))]
    [InlineData("4444442799", typeof(DHCW.PD.Exceptions.TimeoutException))]
    [InlineData("5555542799", typeof(Exception))]
    public void GetByNHSNumber_ShouldThrowExpectedException(string nhsNumber, Type expectedException)
    {
        var ex = Assert.Throws(expectedException, () => _service.GetByNHSNumber(nhsNumber));
        Assert.IsType(expectedException, ex);
    }

    [Fact]
    public void GetByNHSNumber_ShouldReturnBuiltPatient_WhenIdIsValid()
    {
        var validId = "7269370370";
        _validatorMock.Setup(v => v.IsValid(validId)).Returns(true);

        var result = _service.GetByNHSNumber(validId);

        Assert.NotNull(result);
        Assert.Equal(validId, result.Id);
    }

    [Fact]
    public void GetByNHSNumber_ShouldThrowInvalidDataException_WhenIdIsInvalid()
    {
        var invalidId = "0000042799";
        _validatorMock.Setup(v => v.IsValid(invalidId)).Returns(false);

        Assert.Throws<InvalidDataException>(() => _service.GetByNHSNumber(invalidId));
    }
	
	[Fact]
	public void GetByNHSNumber_ShouldReturnDefaultPatient_WhenIdIs8888842799()
	{
		var result = _service.GetByNHSNumber("8888842799");

		Assert.NotNull(result);
		Assert.Equal("8888842799", result.Id);
		Assert.Equal("Holmes", result.Name[0].Family); 
	}
}
