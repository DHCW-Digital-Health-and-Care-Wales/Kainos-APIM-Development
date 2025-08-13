using DHCW.PD.Controllers;
using DHCW.PD.Helpers;
using DHCW.PD.Services;
using DHCW.PD.Validators;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests.Controllers.FHIR.R4
{
    public sealed class PatientControllerTests
    {

        private readonly IPatientService _patientService;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<INhsIdValidator> _validatorMock;
        private readonly Mock<ILogger<PatientController>> _logger;
        private readonly PatientController _controller;

        public PatientControllerTests()
        {
            _logger = new Mock<ILogger<PatientController>>();
            _configurationMock = new Mock<IConfiguration>();
            _validatorMock = new Mock<INhsIdValidator>();
            _patientService = new MpiPatientService(
                _configurationMock.Object,
                _validatorMock.Object,
                new PatientBuilder(),
                new Mock<ILogger<MpiPatientService>>().Object
            );
            

            _controller = new PatientController(_patientService, _logger.Object);
        }

        [Fact]
        public void GetByNHSNumber_ShouldReturnBuiltPatient_WhenIdIsValid()
        {
            // Arrange
            string apiKey = "testkey";
            string authorization = "auth";
            string validId = "4857773457";

            // Setup
            _validatorMock.Setup(v => v.IsValid(validId)).Returns(true);

            ActionResult<Patient> response = _controller.GetByNhsId(apiKey, authorization, validId);

            Assert.NotNull(response);
            Assert.NotNull(response.Result);
            Patient result = TestUtility.GetObjectResultContent<Patient>(response);
        }

        [Fact]
        public void GetByNHSNumber_ShouldThrowInvalidDataException_WhenIdIsInvalid()
        {
            // Arrange
            string apiKey = "testkey";
            string authorization = "auth";
            string invalidId = "0000042799";

            // Setup
            _validatorMock.Setup(v => v.IsValid(invalidId)).Returns(false);

            Assert.Throws<InvalidDataException>(() =>
            {
                ActionResult<Patient> response = _controller.GetByNhsId(apiKey, authorization, invalidId);
            });

        }

        [Fact]
        public void GetByNHSNumber_ShouldThrowTimeoutException_WhenIdIs4444442799()
        {
            // Arrange
            string apiKey = "testkey";
            string authorization = "auth";
            string invalidId = "4444442799";

            Assert.Throws< DHCW.PD.Exceptions.TimeoutException> (() =>
            {
                ActionResult<Patient> response = _controller.GetByNhsId(apiKey, authorization, invalidId);
            });

        }

        [Fact]
        public void GetByNHSNumber_ShouldReturnBuiltPatient_WhenIdIs4857773457()
        {
            // Arrange
            string apiKey = "testkey";
            string authorization = "auth";
            string validId = "4857773457";

            // Setup
            _validatorMock.Setup(v => v.IsValid(validId)).Returns(true);

            ActionResult<Patient> response = _controller.GetByNhsId(apiKey, authorization, validId);

            Assert.NotNull(response);
            Assert.NotNull(response.Result);
            Patient result = TestUtility.GetObjectResultContent<Patient>(response);
            Assert.Equal("4857773457", result.Id);
            Assert.Equal("Holmes", result.Name[0].Family);
        }

    }
}
