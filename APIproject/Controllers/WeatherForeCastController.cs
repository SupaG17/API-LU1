using LU2_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace LU2_project.Controllers;

[ApiController]
[Route("[controller]")]
public class SmarthealthController : ControllerBase
{
    /*private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };*/

    private readonly ILogger<SmarthealthController> _logger;

    public SmarthealthController(ILogger<SmarthealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            /*Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),*/
            UserGuid = Guid.NewGuid(),
            UserEmail = "tesemail@email.com",
            PassWord = "randompassword",
            //TemperatureC = Random.Shared.Next(-20, 55),
            /*Summary = Summaries[Random.Shared.Next(Summaries.Length)]*/
        })
        .ToArray();
    }
}
