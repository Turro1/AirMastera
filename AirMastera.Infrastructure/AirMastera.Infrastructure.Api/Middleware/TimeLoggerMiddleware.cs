using AirMastera.Infrastructure.Data;
using AirMastera.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AirMastera.Infrastructure.Api.Middleware;

public class TimeLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TimeLoggerMiddleware> _logger;
    private readonly AirMasteraDbContext _dbContext;

    public TimeLoggerMiddleware(
        RequestDelegate next,
        ILogger<TimeLoggerMiddleware> logger,
        AirMasteraDbContext dbContext)
    {
        _next = next;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Invoke(HttpContext context)
    {
        var person = new PersonDb
        {
            Id = Guid.NewGuid(),
            FullName = "Вань",
            Phone = $"+373777500{new Random().Next(10, 99)}"
        };
        _dbContext.Persons.Add(person);
        await _dbContext.SaveChangesAsync();
        _logger.LogDebug($"Request start: {DateTime.Now} {person.Phone}");

        await _next.Invoke(context);

        _logger.LogDebug($"Request finish: {DateTime.Now} {person.Phone}");
    }
}