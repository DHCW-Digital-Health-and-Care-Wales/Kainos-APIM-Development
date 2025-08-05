using Hl7.Fhir.Model;
using DHCW.PD.Helpers;
using DHCW.PD.Validators;
using DHCW.PD.Exceptions;

namespace DHCW.PD.Services;

public class MpiPatientService : IPatientService
{
    private readonly INhsIdValidator _nhsIdValidator;
    private readonly PatientBuilder _patientBuilder;
	private readonly ILogger<MpiPatientService> _logger;

    public MpiPatientService(
        INhsIdValidator nhsIdValidator,
        PatientBuilder patientBuilder,
		ILogger<MpiPatientService> logger
    )
    {
        _nhsIdValidator = nhsIdValidator;
        _patientBuilder = patientBuilder;
		_logger = logger;
    }

    public Patient GetByFirstnameSurnameDOB(string firstName, string surname, string dob)
    {
        throw new NotImplementedException();
    }

    public Patient GetByNHSNumber(string nhsNumber)
    {
        return nhsNumber switch
        {
            "1111142799" => throw new BadRequestException(),
            "2222242799" => throw new UnauthorizedException(),
            "3333342799" => throw new ForbiddenException(),
            "4444442799" => throw new DHCW.PD.Exceptions.TimeoutException(),
            "5555542799" => throw new Exception(),
			"8888842799" => _patientBuilder.Build(),
            _ => ValidateIdAndReturnPatient(nhsNumber)
        };
    }

    private Patient ValidateIdAndReturnPatient(string id)
    {
        if (!_nhsIdValidator.IsValid(id))
            throw new InvalidDataException();

        return _patientBuilder
			.Id(id)
			.Build();
    }
}
