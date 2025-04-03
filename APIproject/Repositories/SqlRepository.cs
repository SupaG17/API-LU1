using Dapper;
using LU1_project.Models;
using LU1_project.Interfaces;
using System.Data;

namespace LU1_project.Repositories
{
    public class SqlRepository : ISqlRepository
    {
        private readonly IDbConnection _dbConnection;

        public SqlRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<UserInfo> InsertAsync(UserInfo userInfo)
        {
            var query = @"INSERT INTO [dbo].[User] (UserName, PassWord, CurrentLevel, Avatar, Id) 
                          VALUES (@UserName, @PassWord, @CurrentLevel, @Avatar, @Id)";

            await _dbConnection.ExecuteAsync(query, userInfo);
            return userInfo;
        }

        public async Task<UserInfo?> ReadAsync(Guid id)
        {
            var query = "SELECT * FROM [dbo].[User] WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<UserInfo>(query, new { Id = id });
        }
        public async Task<UserInfo?> ReadAsync(string userName)
        {
            var query = "SELECT * FROM [dbo].[User] WHERE Username = @UserName";
            return await _dbConnection.QuerySingleOrDefaultAsync<UserInfo>(query, new { Username = userName });
        }

        public async Task<IEnumerable<UserInfo>> ReadAllAsync()
        {
            var query = "SELECT * FROM [dbo].[User]";
            return await _dbConnection.QueryAsync<UserInfo>(query);
        }

        public async Task UpdateAsync(UserInfo userInfo)
        {
            var query = @"UPDATE [dbo].[User] 
                          SET CurrentLevel = @CurrentLevel, Avatar = @Avatar 
                          WHERE Id = @Id";

            await _dbConnection.ExecuteAsync(query, userInfo);
        }

        public async Task DeleteAsync(Guid id)
        {
            var query = "DELETE FROM [dbo].[User] WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }
    }
}
