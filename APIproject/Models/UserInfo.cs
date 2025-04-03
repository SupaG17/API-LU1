namespace LU2_project.Models;

public class UserInfo
{
    public string UserName { get; set; }
    public string PassWord { get; set; }
    public int? CurrentLevel { get; set; }
    public int? Avatar { get; set; }
    public Guid Id { get; set; }
    public UserInfo(string userName, string passWord, int? currentLevel, int? avatar) 
    {
        UserName = userName;
        PassWord = passWord;
        CurrentLevel = currentLevel;
        Avatar = avatar;
        Id = Guid.NewGuid();
    }
}
