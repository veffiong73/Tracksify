using Microsoft.EntityFrameworkCore;
using TracksifyAPI;
using TracksifyAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Database Context
builder.Services.AddDbContext<TracksifyDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString"));
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
