using CodeChallenge.API.Profiles;
using CodeChallenge.DataAccess;
using CodeChallenge.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Usar Automapper
builder.Services.AddAutoMapper(options => options.AddProfile<AutoMapperProfiles>());
//Usar servicio de dependencias
builder.Services.AddDependencies();

//Usar valor de conexion obtenido del archivo de configuracion
builder.Services.AddDbContext<CodeChallengeDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CodeChallengeDB"));

    //SOLO HABILITAR EN DESARROLLO
    //options.EnableSensitiveDataLogging();

    // Utiliza el AsNoTracking por default en todos los querys de seleccion
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(); //CORS


builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }
