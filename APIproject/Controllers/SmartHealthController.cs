using LU1_project.Models;
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
        (
            "tesemail@email.com", //UserName
            "randompassword", //PassWord
            3, //CurrentLevel
            2 //Avatar
        )
            
        )
        .ToArray();
    }
}
