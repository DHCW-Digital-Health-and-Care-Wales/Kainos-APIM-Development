using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

public sealed class ContractTest
{

	private WebApplicationFactory<Program> _factory;

    public ContractTest()
	{
		_factory = new WebApplicationFactory<Program>();
    }

    [Fact]
	public async Task Search_NHSNumber_RecordFound()
	{
		using HttpClient client = _factory.CreateClient();

		// Perform action
		client.DefaultRequestHeaders.Add("ApiKey", "TestApiKey");
		HttpResponseMessage result = await client.GetAsync("/FHIR/R4/Patient/4857773457");

		// Assert
		Assert.Equal(HttpStatusCode.OK, result.StatusCode);
	}

	[Fact]
	public async Task Search_NHSNumber_RecordNotFound()
	{
		using HttpClient client = _factory.CreateClient();

		// Perform action
		client.DefaultRequestHeaders.Add("ApiKey", "TestApiKey");
		HttpResponseMessage result = await client.GetAsync("/FHIR/R4/Patient/7777742799");

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
	}

	[Fact]
	public async Task Search_NHSNumber_Timeout()
	{
		using HttpClient client = _factory.CreateClient();

		// Perform action
		client.DefaultRequestHeaders.Add("ApiKey", "TestApiKey");
		HttpResponseMessage result = await client.GetAsync("/FHIR/R4/Patient/1111142799");

		// Assert
		Assert.Equal(HttpStatusCode.RequestTimeout, result.StatusCode);
	}

	[Fact]
	public async Task Search_NHSNumber_InvalidNHSNumber()
	{
		using HttpClient client = _factory.CreateClient();

		// Perform action
		client.DefaultRequestHeaders.Add("ApiKey", "TestApiKey");
		HttpResponseMessage result = await client.GetAsync("/FHIR/R4/Patient/28");

		// Assert
		Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
	}

	[Fact]
	public async Task Search_NHSNumber_InternalServerError()
	{
		using HttpClient client = _factory.CreateClient();

		// Perform action
		client.DefaultRequestHeaders.Add("ApiKey", "TestApiKey");
		HttpResponseMessage result = await client.GetAsync("/FHIR/R4/Patient");

		// Assert
		Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
	}

	[Fact]
	public async Task Search_NHSNumber_Unauthorised()
	{
		using HttpClient client = _factory.CreateClient();

		// Perform action
		HttpResponseMessage result = await client.GetAsync("/FHIR/R4/Patient/8888842799");

		// Assert
		Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
	}

}
