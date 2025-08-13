using DHCW.PD.Helpers;
using DHCW.PD.Middlewares;
using DHCW.PD.Services;
using DHCW.PD.Validators;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<INhsIdValidator, NhsIdValidator>();
builder.Services.AddSingleton<PatientBuilder>();
builder.Services.AddSingleton<IPatientService, MpiPatientService>();
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSerilogRequestLogging();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }