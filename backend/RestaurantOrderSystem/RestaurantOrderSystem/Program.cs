using Microsoft.EntityFrameworkCore;
using RestaurantOrderSystem;
using RestaurantOrderSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
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


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbSeeder.Seed(context);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "RestaurantOrderSystem API");
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseStaticFiles(); // für Bilder aus wwwroot/images

app.UseAuthorization();

app.MapControllers();

app.Run();
