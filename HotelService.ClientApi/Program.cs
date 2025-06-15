using HotelService.ClientApi.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Dodaj kontrolery + obsługę PATCH
builder.Services
    .AddControllers()
    .AddNewtonsoftJson(); // ← wspiera JsonPatchDocument

// Swagger + XML-komentarze
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "HotelService API",
        Version = "v1"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Dodaj serwisy klienta
builder.Services.AddClientServices();

var app = builder.Build();

// Zawsze pokazuj Swaggera (nie tylko w Development)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelService API v1");
    c.RoutePrefix = "swagger"; // Swagger dostępny na "/"
});

app.UseAuthorization();
app.MapControllers();
app.Run();
