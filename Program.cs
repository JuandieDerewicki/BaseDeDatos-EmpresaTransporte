using Microsoft.EntityFrameworkCore;
using ViajePlusBDAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Se obtiene la cadena de conexion a la BD desde la config de la aplicacion, mientras la cadena se almccena en el appsetings .json y se recupera aca para configurar la BD
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Estamos compartiendo la cadena de conexion a toda la API

//// Luego se configura EF para utilizar la cadena de conexion especificada para establecer una conexion a la BD SQL Server
builder.Services.AddDbContext<MiDbContext>(options =>
options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var misReglasCors = "ReglasCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: misReglasCors, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Agregar servicios y dependencias
//builder.Services.AddScoped<IUsuarioService, UsuarioService>();
//builder.Services.AddScoped<IItinerarioService, ItinerarioService>();
//builder.Services.AddScoped<IServicioService, ServicioService>();
//builder.Services.AddScoped<IPuntoIntermedioService, PuntoIntermedioService>();
//builder.Services.AddScoped<IServicioUsuarioService, ServicioUsuarioService>();
//builder.Services.AddScoped<IItinerarioPuntoIntermedioService, ItinerarioPuntoIntermedioService>();
//builder.Services.AddScoped<IUnidadTransporteService, UnidadTransporteService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseCors(misReglasCors);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
