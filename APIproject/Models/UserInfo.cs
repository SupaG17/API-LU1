namespace LU2_project.Models;

public class UserInfo
{
    public required string UserName { get; set; }
    public required string PassWord { get; set; }
    public int? CurrentLevel { get; set; }
    public int? Avatar { get; set; }
    public Guid Id { get; internal set; }
}
