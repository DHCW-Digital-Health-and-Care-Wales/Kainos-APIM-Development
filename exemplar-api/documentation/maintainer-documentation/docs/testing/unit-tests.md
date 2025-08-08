# Unit Tests

This guide explains how to write unit tests using xUnit and Moq, and how to run them using Docker Compose for the **Patient Demographics API**.

---

## Writing Unit Tests

### Project Structure
Ensure your unit test project is located at:
```
test/unit/UnitTests.csproj
```

### Dependencies
Add the following NuGet packages to your test project:

```bash
dotnet add package xunit
dotnet add package Moq
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package xunit.runner.visualstudio
```

### Example Test Using xUnit and Moq

```csharp
using Xunit;
using Moq;
using PatientDemographicsAPI.Services;
using PatientDemographicsAPI.Models;

public class PatientServiceTests
{
    [Fact]
    public void GetPatient_ReturnsCorrectPatient()
    {
        // Arrange
        var mockRepo = new Mock<IPatientRepository>();
        mockRepo.Setup(repo => repo.GetPatientById(1))
                .Returns(new Patient { Id = 1, Name = "John Doe" });

        var service = new PatientService(mockRepo.Object);

        // Act
        var result = service.GetPatient(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("John Doe", result.Name);
    }
}
```

---

## Running Tests

### Running the Tests using Docker Compose

To run the tests, execute:

```bash
docker compose run --rm unit-tests
```

This will:
- Build the test project using the `build` target in your Dockerfile.
- Run the tests using `dotnet test`.
- Output results to `./test-results/unit/test-results.trx`.

### Running the Tests using dotnet cli

To run the tests, execute:

```bash
dotnet test test/unit/UnitTests.csproj
```

---

## Viewing Test Results

You can view the `.trx` test results using:
- Visual Studio (Test > Windows > Test Explorer > Open Test Result File)
- ReportGenerator for HTML reports

---

## Tips

- Use `[Theory]` and `[InlineData]` for parameterized tests.
- Use `MockBehavior.Strict` to ensure all expectations are met.
- Unit test project structure must match source directory structure.

