using Hl7.Fhir.Model;

namespace DHCW.PD.Services;

[ExcludeFromCodeCoverage]
public interface IPatientService
{
    public Patient GetByNHSNumber(string nhsNumber);
    public Patient GetByFirstnameSurnameDOB(string firstName, string surname, string dob);
}
