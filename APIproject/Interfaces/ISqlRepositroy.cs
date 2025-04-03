using LU1_project.Models;
namespace LU1_project.Interfaces
{
    public interface ISqlRepository
    {
        Task UpdateAsync(UserInfo userInfo);
            
    }
}
