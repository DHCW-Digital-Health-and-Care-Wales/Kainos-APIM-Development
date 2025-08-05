using Hl7.Fhir.Model;
using DHCW.PD.Helpers;
using DHCW.PD.Validators;
using DHCW.PD.Exceptions;

namespace DHCW.PD.Services;

public class MpiPatientService : IPatientService
{
    private IValidator<string, bool> _nhsIdValidator;
    private PersonBuilder _personBuilder;

    private string _hostname;
    private int _port;

    public MpiPatientService(
        IConfiguration configuration,
        NhsIdValidator nhsIdValidator,
        PersonBuilder personBuilder
    )
    {
        _nhsIdValidator = nhsIdValidator;
        _personBuilder = personBuilder;

        _hostname = configuration["MPI:Hostname"];
        _port = int.Parse(configuration["MPI:Port"]);
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
            _ => ValidateIdAndReturnPatient(nhsNumber)
        };
    }

    private Patient ValidateIdAndReturnPatient(string id)
    {
        if (!_nhsIdValidator.IsValid(id))
        {
            throw new InvalidDataException();
        }

        return _personBuilder.Build();
    }
}
