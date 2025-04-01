using LU2_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace LU2_project.Controllers;

[ApiController]
[Route("[controller]")]
public class SmartHealthController : ControllerBase
{
    private readonly ILogger<SmartHealthController> _logger;

    public SmartHealthController(ILogger<SmartHealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetUserInfo")]
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
