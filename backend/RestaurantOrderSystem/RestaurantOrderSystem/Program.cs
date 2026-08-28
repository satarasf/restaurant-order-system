using Microsoft.EntityFrameworkCore;
using RestaurantOrderSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//EF Core mit SQLite registrieren
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite("Data Source=restaurant.db"));

// CORS für Sveltekit
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseStaticFiles(); // für Bilder aus wwwroot/images

app.UseAuthorization();

app.MapControllers();

app.Run();
