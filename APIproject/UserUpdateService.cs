using LU1_project.Interfaces;
using LU1_project.Models;
namespace LU1_project.UserUpdateService;
public class UserUpdateService
{
    private readonly ISqlRepository _userRepository;

    public UserUpdateService(ISqlRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task OnEventTriggered(UserInfo userInfo)
    {
        await _userRepository.UpdateAsync(userInfo);
    }
}
