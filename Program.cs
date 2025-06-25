// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

// app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

// app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
// using Microsoft.OpenApi.Models;
// using Swashbuckle.AspNetCore;

// using TodoApi.AppDataContext;
// using TodoApi.Models;
// using TodoApi.Middleware;
// using Microsoft.EntityFrameworkCore;



// using Swashbuckle.AspNetCore.SwaggerGen;
// using Swashbuckle.AspNetCore.SwaggerUI;
// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers();

// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();



//  // Add  This to in the Program.cs file
// builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings")); // Add this line
// builder.Services.AddSingleton<TodoDbContext>();
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Add this line
// builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
// builder.Services.AddProblemDetails();
// builder.Services.AddLogging();

// builder.Services.AddDbContext<TodoDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    

// var app = builder.Build();

// // Add this line

// // {
// //     using var scope = app.Services.CreateScope(); // Add this line
// //     var context = scope.ServiceProvider; // Add this line
// // }using (var scope = app.Services.CreateScope())
// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
//     db.Database.EnsureCreated(); // or db.Database.Migrate();
// }
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();
// app.UseExceptionHandler();
// app.UseAuthorization();

// app.MapControllers();

// app.Run();
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using TodoApi.AppDataContext;
using TodoApi.Middleware;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Get connection string early for debugging
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"Connection string: {connectionString ?? "NULL"}");

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with SQL Server
// builder.Services.AddDbContext<TodoDbContext>(options =>
//     options.UseSqlServer(connectionString));
    builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add other services
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddLogging();

var app = builder.Build();

// Create database if not exists
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
    db.Database.EnsureCreated(); // or use db.Database.Migrate();
}

// Middleware setup
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseAuthorization();

app.MapControllers();

app.Run();
