using DHCW.PD.Controllers;
using DHCW.PD.Services;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;

namespace UnitTests.Controllers.FHIR.R4
{
    public class PatientControllerTests
    {

        private readonly Mock<IPatientService> _patientService;
        private readonly Mock<ILogger<PatientController>> _logger;
        private readonly PatientController _controller;

        public PatientControllerTests()
        {
            _patientService = new Mock<IPatientService>();
            _logger = new Mock<ILogger<PatientController>>();

            _controller = new PatientController(_patientService.Object, _logger.Object);
        }

        [Fact]
        public void GetByNHSNumber_ShouldReturnBuiltPatient_WhenIdIsValid()
        {
            // Arrange
            string apiKey = "testkey";
            string authorization = "auth";
            string validId = "4857773457";

            _patientService.Setup(x => x.GetByNHSNumber(validId)).Returns(new Patient() { Id= validId });
            
            ActionResult<Patient> response = _controller.GetByNhsId(apiKey, authorization, validId);

            Assert.NotNull(response);
            Assert.NotNull(response.Result);
            Patient p = TestUtility.GetObjectResultContent<Patient>(response);
            Assert.True(p.Id == validId);
        }


    }
}
