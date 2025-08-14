using DHCW.PD.Configuration;
using DHCW.PD.Exceptions;
using DHCW.PD.Helpers;
using DHCW.PD.Validators;
using Hl7.Fhir.Model;

namespace DHCW.PD.Services;

public class MpiPatientService : IPatientService
{
    private readonly INhsIdValidator _nhsIdValidator;
    private readonly PatientBuilder _patientBuilder;
    private readonly ILogger<MpiPatientService> _logger;
    private readonly MPIServiceConfiguration _configuration;

    public MpiPatientService(
        MPIServiceConfiguration configuration,
        INhsIdValidator nhsIdValidator,
        PatientBuilder personBuilder,
        ILogger<MpiPatientService> logger
    )
    {
        _nhsIdValidator = nhsIdValidator;
        _patientBuilder = personBuilder;
        _logger = logger;
        _configuration = configuration;
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
            "4857773457" => _patientBuilder.Build(),
            _ => ValidateIdAndReturnPatient(nhsNumber)
        };
    }

    private Patient ValidateIdAndReturnPatient(string id)
    {
		_logger.LogDebug("Validating Patient NHS number: {NhsNumber}", id);
        if (!_nhsIdValidator.IsValid(id))
		{
			_logger.LogError("Invalid NHS number: {NhsNumber}", id);
            throw new InvalidDataException();
		}
		_logger.LogDebug("Patient NHS number: {NhsNumber} is valid", id);

		_logger.LogInformation("Calling MPI service to fetch Patient data for Patient NHS number: {NhsNumber}", id);

        Patient patient = _patientBuilder
            .Id(id)
            .Build();

		_logger.LogDebug("Patient data fetched from MPI service for Patient NHS number: {NhsNumber}", id);

		return patient;
    }
}
