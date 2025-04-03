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
                var userId = await sqlConnection.ExecuteAsync("INSERT INTO [dbo].[UserLU1] (UserName, PassWord, CurrentLevel, Id) values(@UserName, @PassWord, @CurrentLevel, @Id)", userInfo);
                return userInfo;
            }
        }
        

        public async Task<UserInfo?> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<UserInfo>("SELECT * FROM [dbo].[UserLU1] WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<UserInfo>> ReadAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<UserInfo>("SELECT * FROM [dbo].[UserLU1]");
            }
        }

        public async Task UpdateAsync(UserInfo userInfo)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [dbo].[UserLU1] SET " +
                                                 "CurrentLevel = @CurrentLevel"
                                                 , userInfo);

            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [dbo].[UserLU1] WHERE Id = @Id", new { id });
            }
        }

    }
}
