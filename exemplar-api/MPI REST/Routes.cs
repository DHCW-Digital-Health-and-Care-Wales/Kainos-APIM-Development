using LookupServices;
using Microsoft.AspNetCore.Mvc;

namespace DemographicsREST
{
    public static class Routes
    {
        public static void Register(WebApplication? app)
        {
            if (app is null)
                return;

            app.MapGet("/patient/{nhsNumber}",
                async (IPatientLookup lookup, string nhsNumber, [FromHeader] string? ApiKey) =>
                {

                    if (string.IsNullOrEmpty(ApiKey))
                    {
                        return Results.Unauthorized();
                    }

                    if (string.IsNullOrEmpty(nhsNumber))
                    {
                        return Results.StatusCode(500);
                    }

                    // Abstract the lookup of the patient
                    Hl7.Fhir.Model.Patient? patient;
                    try
                    {
                        patient = await lookup.ByNHSNumber(nhsNumber);
                    }
                    catch (TimeoutException)
                    {
                        return Results.StatusCode(408);
                    }
                    catch (InvalidDataException)
                    {
                        return Results.BadRequest("Invalid NHS Number");
                    }
                    catch (Exception ex)
                    {
                        // Log the exception (not implemented here)
                        return Results.StatusCode(500);
                    }

                    if (patient is not null)
                    {
                        return TypedResults.Ok(patient);
                    }
                    else
                    {
                        return Results.NotFound("Patient not found");
                    }

                })
                .Produces<Hl7.Fhir.Model.Patient>(StatusCodes.Status200OK)
                .Produces<string>(StatusCodes.Status400BadRequest)
                .Produces<string>(StatusCodes.Status401Unauthorized)
                .Produces<string>(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status404NotFound)
                .Produces<string>(StatusCodes.Status408RequestTimeout)
                .Produces<string>(StatusCodes.Status500InternalServerError)
                .WithName("getPatientById")
                .WithDescription("Returns a FHIR compliant Patient resource. More details can be found in FHIR documentation at https://www.hl7.org/fhir/patient.html")
                .WithTags("Patient")
                .WithOpenApi();
        }
    }
}
