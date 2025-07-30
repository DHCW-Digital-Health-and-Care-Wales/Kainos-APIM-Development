using MPI_REST;
using Hl7.Fhir.Rest;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/patient/{nhsNumber}",
    async (string nhsNumber, [FromHeader] string? ApiKey) =>
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
         patient = await Utility.LookupPatientOnNHSNumber(nhsNumber);
    }
    catch (TimeoutException)
    {
        return Results.StatusCode(408);
    }
    catch(InvalidDataException)
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

app.Run();

public partial class Program
{
    // This partial class is used to allow the test project to access the Program class.
    // It is not necessary for the application to function.
}