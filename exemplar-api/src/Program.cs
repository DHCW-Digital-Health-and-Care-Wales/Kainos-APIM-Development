using DHCW.PD.Validators;
using DHCW.PD.Services;
using DHCW.PD.Helpers;
using Serilog;
using DemographicsREST.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.Configure<MPIServiceConfiguration>(builder.Configuration.GetSection("MPIServiceConfiguration"));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<NhsIdValidator>();
builder.Services.AddSingleton<PatientBuilder>();
builder.Services.AddSingleton<IPatientService, MpiPatientService>();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }