using Microsoft.AspNetCore.RateLimiting;
using Scalar.AspNetCore;
using TimelogAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddSlidingWindowLimiter("sliding_window", config =>
    {
        config.Window = TimeSpan.FromMinutes(1);
        config.SegmentsPerWindow = 6;
        config.PermitLimit = 60;
    });
});

builder.Services.AddOpenApi();

builder.Services.AddHybridCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class  Program { } // Must be at the bottom of the file to be able to access the Program class from the test project.