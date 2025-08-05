using Hl7.Fhir.Model;

namespace DHCW.PD.Helpers;

public class PersonBuilder
{
    private string family = "Holmes";
    private string given = "Sherlock";
    private string birthDate = "19800413";
    private AdministrativeGender gender = AdministrativeGender.Male;
    private Address address = new Address
    {
        LineElement = new List<FhirString>
        {
            new FhirString("221B Baker Street"),
            new FhirString("Marylebone")
        },
        City = "London",
        PostalCode = "NW1 6XE"
    };
    private string id = "8888842799";

    public PersonBuilder Family(string data) { this.family = data; return this; }
    public PersonBuilder Given(string data) { this.given = data; return this; }
    public PersonBuilder BirthDate(string data) { this.birthDate = data; return this; }
    public PersonBuilder Gender(AdministrativeGender data) { this.gender = data; return this; }
    public PersonBuilder Address(Address data) { this.address = data; return this; }
    public PersonBuilder Id(string data) { this.id = data; return this; }

    public Patient Build()
    {
        return new Patient
        {
            Name = new List<HumanName>
            {
                new HumanName
                {
                    Family = this.family,
                    Given = new List<string> { this.given }
                }
            },
            BirthDate = this.birthDate,
            Gender = this.gender,
            Address = new List<Address> { this.address },
            Id = this.id
        };
    }
}
