using Hl7.Fhir.Model;
using System.Diagnostics.CodeAnalysis;

namespace DHCW.PD.Helpers;

[ExcludeFromCodeCoverage]
public sealed class PatientBuilder
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
    private string id = "4857773457";

    public PatientBuilder Family(string data) { family = data; return this; }
    public PatientBuilder Given(string data) { given = data; return this; }
    public PatientBuilder BirthDate(string data) { birthDate = data; return this; }
    public PatientBuilder Gender(AdministrativeGender data) { gender = data; return this; }
    public PatientBuilder Address(Address data) { address = data; return this; }
    public PatientBuilder Id(string data) { id = data; return this; }

    public Patient Build()
    {
        return new Patient
        {
            Name = new List<HumanName>
            {
                new HumanName
                {
                    Family = family,
                    Given = new List<string> { given }
                }
            },
            BirthDate = birthDate,
            Gender = gender,
            Address = new List<Address> { address },
            Id = id
        };
    }
}
