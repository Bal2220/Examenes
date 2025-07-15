using System.Reflection;
using Example_EF_1.EasyIrriot.Application.CommandService;
using Example_EF_1.EasyIrriot.Application.QueryService;
using Example_EF_1.EasyIrriot.Domain;
using Example_EF_1.EasyIrriot.Domain.Models.Commands;
using Example_EF_1.EasyIrriot.Domain.Services;
using Example_EF_1.EasyIrriot.Infraestructure;
using Example_EF_1.Shared.Domain.Repositories;
using Example_EF_1.Shared.Infraestructure.Persistence.Configuration;
using Example_EF_1.Shared.Infraestructure.Persistence.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Verify Database Connection String
if (connectionString is null)
    // Stop the application if the connection string is not set.
    throw new Exception("Database connection string is not set.");

// Configure Database Context and Logging Levels
if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<EasyIrriotContext>(
        options =>
        {
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });
else if (builder.Environment.IsProduction())
    builder.Services.AddDbContext<EasyIrriotContext>(
        options =>
        {
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Error)
                .EnableDetailedErrors();
        });

// Configure Dependency Injection

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IThingsRepository, ThingsRepository>();
builder.Services.AddScoped<IThingStatesRepository, ThingStatesRepository>();
builder.Services.AddScoped<IThingsCommandService, ThingsCommandService>();
builder.Services.AddScoped<IThingsQueryService, ThingsQueryService>();
builder.Services.AddScoped<IThingStatesCommandService, ThingStatesCommandService>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateThingsCommand>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateThingStatesCommand>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "EasyIrriot App",
        Description = "APIS to handle data for things and thingStates",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "María Hernández",
            Email = "u202311258@upc.edu.pe",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<EasyIrriotContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();