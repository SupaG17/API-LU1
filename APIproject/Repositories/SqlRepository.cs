using Dapper;
using LU2_project.Models;
using Microsoft.Data.SqlClient;

namespace LU2_project.Repositories
{
    public class SqlRepository
    {
        private readonly string sqlConnectionString;

        public SqlRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<UserInfo> InsertAsync(UserInfo userInfo)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var userId = await sqlConnection.ExecuteAsync("INSERT INTO [dbo].[User] (UserName, PassWord, CurrentLevel, Avatar, Id) values(@UserName, @PassWord, @CurrentLevel, @Avatar, @Id)", userInfo);
                return userInfo;
            }
        }

        public async Task<UserInfo?> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<UserInfo>("SELECT * FROM [dbo].[User] WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<UserInfo>> ReadAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<UserInfo>("SELECT * FROM [dbo].[User]");
            }
        }

        public async Task UpdateAsync(UserInfo userInfo)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [dbo].[User] SET " +
                                                 "CurrentLevel = @CurrentLevel, Avatar = @Avatar"
                                                 , userInfo);

            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [dbo].[User] WHERE Id = @Id", new { id });
            }
        }
        
    }
}
