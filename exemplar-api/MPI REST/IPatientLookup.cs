using Hl7.Fhir.Model;

namespace LookupServices
{
    public interface IPatientLookup
    {
        public System.Threading.Tasks.Task<Patient?> ByNHSNumber(string nhsNumber);
        public System.Threading.Tasks.Task<Patient?> ByFirstnameSurnameDOB(string firstName, string surname, string dob);
    }
}
