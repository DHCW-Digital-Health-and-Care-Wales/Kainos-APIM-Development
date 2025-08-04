using Hl7.Fhir.Model;

namespace DHCW.PD.Services;

public interface IPatientService
{
	public Patient GetByNHSNumber(string nhsNumber);
	public Patient GetByFirstnameSurnameDOB(string firstName, string surname, string dob);
}
