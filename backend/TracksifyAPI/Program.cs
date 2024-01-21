using Microsoft.EntityFrameworkCore;
using TracksifyAPI;
using TracksifyAPI.Data;
using TracksifyAPI.Interfaces;
using TracksifyAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions.Common;
using Microsoft.OpenApi.Models;
using TracksifyAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();
// Registering Dependency Injection for UserRepository
builder.Services.AddScoped<IUserRepository, UserRepository>();
// Registering Dependency Injection for ProjectRepository
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectUpdateRepository, ProjectUpdateRepository>();
// Registering Dependency Injection for TokenRepository
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Addition of API explorer for endpoints
builder.Services.AddEndpointsApiExplorer();

// Addition of Swagger generator to the services collection
builder.Services.AddSwaggerGen(options =>
{
    // Defining the Swagger document for Tracksify API version
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "TracksifyAPI", Version = "v1" });

    // Defining the security scheme based on JWT bearer authentication
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    // Applying the security requirement globally to all API operations
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "OAuth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});


//Registering Dependency Injection for Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
});

builder.Services.AddControllers().AddNewtonsoftJson( options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// Add Database Context
builder.Services.AddDbContext<TracksifyDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString"));
});

builder.Services.AddSingleton<IEmailService>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<EmailService>>();
    var environment = provider.GetRequiredService<IWebHostEnvironment>();

    var templatesFolderPath = Path.Combine(environment.ContentRootPath, "Emails");

    return new EmailService(templatesFolderPath, logger, builder.Configuration);
});

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seed")
    SeedData(app);

async void SeedData(IHost app)
{

    using (var scope = app.Services.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        await service!.SeedDataContext();
    }
}

app.UseSwagger();
app.UseSwaggerUI();
/*// Configure the HTTP requegit st pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwaggerUI();
}*/

// Redirecting HTTP requests to HTTPS
app.UseHttpsRedirection();

// Using the authentication middleware 
app.UseAuthentication();

// Using the authorization middleware
app.UseAuthorization();

// Mapping the controllers in the application
app.MapControllers();


// this runs the application
app.Run();

