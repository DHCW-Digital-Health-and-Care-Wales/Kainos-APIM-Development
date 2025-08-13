using DHCW.PD.Exceptions;
using DHCW.PD.Helpers;
using DHCW.PD.Validators;
using Hl7.Fhir.Model;

namespace DHCW.PD.Services;

public sealed class MpiPatientService : IPatientService
{
    private readonly INhsIdValidator _nhsIdValidator;
    private readonly PatientBuilder _patientBuilder;
    private readonly ILogger<MpiPatientService> _logger;

    private string _hostname;
    private int _port;

    public MpiPatientService(
        IConfiguration configuration,
        INhsIdValidator nhsIdValidator,
        PatientBuilder personBuilder,
        ILogger<MpiPatientService> logger
    )
    {
        _nhsIdValidator = nhsIdValidator;
        _patientBuilder = personBuilder;
        _logger = logger;

        IConfigurationSection section = configuration.GetSection("MPIServiceConfiguration");
        _hostname = section["Hostname"] ?? "localhost";
        _port = int.Parse(section["Port"] ?? "9001");
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
        if (!_nhsIdValidator.IsValid(id))
            throw new InvalidDataException();

        return _patientBuilder
            .Id(id)
            .Build();
    }
}
