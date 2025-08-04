using DHCW.PD.Validators;
using DHCW.PD.Services;
using DHCW.PD.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IPatientService, MpiPatientService>();
builder.Services.AddSingleton<NhsIdValidator>();
builder.Services.AddSingleton<PersonBuilder>();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();


app.Run();