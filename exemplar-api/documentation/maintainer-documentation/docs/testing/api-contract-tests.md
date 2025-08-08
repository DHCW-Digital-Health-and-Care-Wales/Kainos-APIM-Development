# API Contract Tests

This guide explains how to write and run API contract tests using XUnit, verifying that the API adheres to expected contracts (e.g., response structure, status codes, headers).

---

## Writing API Contract Tests

### Project Structure
Place your contract tests in a dedicated test project:

```
test/api-contract/
├── ApiContractTests.csproj
└── ContractTests/
    └── PatientControllerTest.cs
```

### Dependencies

Install the following NuGet packages:

```bash
dotnet add package xunit
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package xunit.runner.visualstudio
dotnet add package FluentAssertions
dotnet add package System.Net.Http.Json
```

### Example Test: `PatientEndpointTests.cs`

```csharp
using System.Net;
using System.Net.Http.Json;
using Xunit;
using FluentAssertions;

public class PatientEndpointTests
{
    private readonly HttpClient _client;

    public PatientEndpointTests()
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:3000")
        };
    }

    [Fact]
    public async Task GetPatientById_ShouldReturnValidContract()
    {
        var response = await _client.GetAsync("/Patient/1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Headers.ContentType!.MediaType.Should().Be("application/fhir+json");

        var json = await response.Content.ReadFromJsonAsync<PatientDto>();
        json.Should().NotBeNull();
        json!.Id.Should().Be(1);
        json.Name.Should().NotBeNullOrWhiteSpace();
    }

    private class PatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
```

---

## Running Tests
### Running Tests with .NET CLI

Navigate to the root of your repo and run:

```bash
dotnet test test/api-contract/ApiContractTests.csproj
```

To generate a `.trx` report:

```bash
dotnet test test/api-contract/ApiContractTests.csproj --logger "trx;LogFileName=test-results.trx"
```

---

### Running Tests with Docker Compose

```bash
docker compose run --rm api-contract-tests
```

---

## Tips

- Use `FluentAssertions` for expressive assertions.
- Validate headers, content types, and schema structure.
- Use `[Theory]` with `[InlineData]` for multiple contract scenarios.
- Consider using ApprovalTests for snapshot-style contract validation.
- For larger objects, there is no need to validate all data inside, just validate key identifying information, and the response structure.
- Must never use existing data. Each test should create it's own data to avoid shared state between test cases and test suites.

