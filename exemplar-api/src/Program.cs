using DHCW.PD.Validators;
using DHCW.PD.Services;
using DHCW.PD.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IPatientService, MpiPatientService>();
builder.Services.AddSingleton<INhsIdValidator, NhsIdValidator>();
builder.Services.AddSingleton<PatientBuilder>();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();

// TODO: remove after test containers is added
public partial class Program { }