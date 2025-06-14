using HotelService.ClientApi.Extensions;
using HotelService.ReservationApi.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Dodaj wsparcie dla PATCH (JsonPatchDocument)
builder.Services
    .AddControllers()
    .AddNewtonsoftJson(); // Obs³uga JsonPatchDocument

builder.Services.AddEndpointsApiExplorer();

// ? TYLKO JEDNO AddSwaggerGen, z pe³n¹ konfiguracj¹
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

builder.Services.AddReservationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelService API v1");
        c.RoutePrefix = string.Empty; // Swagger na http://localhost:<port>/
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();
