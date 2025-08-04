using Hl7.Fhir.Model;
using LookupServices;

namespace MPILookupServices
{
    public class PatientLookup : IPatientLookup
    {

        private string _hostname;
        private int _port;

        public PatientLookup(string hostname, int port)
        {
            _hostname = hostname;
            _port = port;
        }

        public async System.Threading.Tasks.Task<Patient?> ByFirstnameSurnameDOB(string firstName, string surname, string dob)
        {

            await System.Threading.Tasks.Task.Yield();
            throw new NotImplementedException();
        }

        public async System.Threading.Tasks.Task<Patient?> ByNHSNumber(string nhsNumber)
        {

            await System.Threading.Tasks.Task.Yield();

            if (nhsNumber == "8888842799") // Test record
            {
                Patient patient = new Patient()
                {
                    Name = new List<HumanName>
                    {
                        new HumanName
                        {
                            Family = "Holmes",
                            Given = new List<string> { "Sherlock" }
                        }
                    },
                    BirthDate = "19800413",
                    Gender = AdministrativeGender.Male,
                    Address = new List<Address>()
                    {
                        new Address()
                        {
                            LineElement = new List<FhirString>()
                            {
                                new FhirString("221B Baker Street"),
                                new FhirString("Marylebone")
                            },
                            City = "London",
                            PostalCode = "NW1 6XE"
                        }
                    },
                    Id = "8888842799"
                };

                return patient;
            }
            else if (nhsNumber == "1111142799") // Simulated timeout for testing purposes
            {
                await System.Threading.Tasks.Task.Delay(2000);
                throw new TimeoutException("Simulated timeout for testing purposes.");
            }
            else if (nhsNumber == "7777742799") // Simulated patient not found
            {
                return null;
            }

            if (!IsValidNHSNumber(nhsNumber))
            {
                throw new InvalidDataException("Invalid NHS Number");
            }

            // MPI Lookup would happen here in a real application

            return null;

        }

        private static bool IsValidNHSNumber(string value)
        {

            if (value.Length != 10)
                return false;

            int total = 0;
            int checkValue = Convert.ToInt32(value[value.Length - 1]);

            for (int counter = 0; counter < 10; counter++)
            {
                char c = value[counter];
                int dv = Convert.ToInt32(c);
                int m = 10 - counter;
                total += (dv * m);
            }

            int remainder = total % 11;
            int check = 11 - remainder;

            if (check == 11)
                check = 0;
            else if (check == 10)
                return false;

            if (check == checkValue)
                return true;
            else
                return false;

        }
    }
}
