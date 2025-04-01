using LU2_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace LU2_project.Controllers;

[ApiController]
[Route("[controller]")]
public class SmarthealthController : ControllerBase
{
    private readonly ILogger<SmarthealthController> _logger;

    public SmarthealthController(ILogger<SmarthealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<UserInfo> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new UserInfo
        {
            UserName = "tesemail@email.com",
            PassWord = "randompassword",
            CurrentLevel = 0,
            Id = Guid.NewGuid(),
        })
        .ToArray();
    }
}
