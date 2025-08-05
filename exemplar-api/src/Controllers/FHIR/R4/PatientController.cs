using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Mvc;
using DHCW.PD.Services;
using DHCW.PD.Exceptions;
using DHCW.PD.Helpers;

namespace DHCW.PD.Controllers;

[ApiController]
[Route("FHIR/R4/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly ILogger<PatientController> _logger;

    public PatientController(
        IPatientService patientService,
        ILogger<PatientController> logger
    )
    {
        _logger = logger;
        _patientService = patientService;
    }

    [HttpGet("{id}")]
    public ActionResult<Patient> GetByNhsId(
        [FromHeader(Name = "x-api-key")] string apiKey,
        [FromHeader(Name = "authorization")] string authorization,
        string id
    )
    {
        ExceptionHelper.ExecuteThrowableIfEmptyOrNull(authorization, () => throw new UnauthorizedException());
        ExceptionHelper.ExecuteThrowableIfEmptyOrNull(apiKey, () => throw new ForbiddenException());
        ExceptionHelper.ExecuteThrowableIfEmptyOrNull(id, () => throw new BadRequestException());

        Patient patient = _patientService.GetByNHSNumber(id);

        return Ok(patient);
    }
}
