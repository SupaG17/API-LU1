﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using LU1_project.Models;
using LU1_project.Repositories;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace LU1_project.Tests
{
    public class SqlRepositoryTests
    {
        private readonly Mock<IDbConnection> _mockDbConnection;
        private readonly SqlRepository _sqlRepository;

        public SqlRepositoryTests()
        {
            _mockDbConnection = new Mock<IDbConnection>();
            _sqlRepository = new SqlRepository(_mockDbConnection.Object);
        }

        [Fact]
        public async Task InsertAsync_InsertsUserInfo_ReturnsUserInfo()
        {
            // Arrange
            var userInfo = new UserInfo("testUser", "testPass", 1, 2);
            var query = @"INSERT INTO [dbo].[User] (UserName, PassWord, CurrentLevel, Avatar, Id) 
                          VALUES (@UserName, @PassWord, @CurrentLevel, @Avatar, @Id)";
            _mockDbConnection.Setup(db => db.ExecuteAsync(query, userInfo, null, null, null))
                             .Returns(Task.FromResult(1));

            // Act
            var result = await _sqlRepository.InsertAsync(userInfo);

            // Assert
            Assert.Equal(userInfo, result);
            _mockDbConnection.Verify(db => db.ExecuteAsync(query, userInfo, null, null, null), Times.Once);
        }

        [Fact]
        public async Task ReadAsync_WithId_ReturnsUserInfo()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userInfo = new UserInfo("testUser", "testPass", 1, 2) { Id = id };
            var query = "SELECT * FROM [dbo].[User] WHERE Id = @Id";
            _mockDbConnection.Setup(db => db.QuerySingleOrDefaultAsync<UserInfo>(query, new { Id = id }, null, null, null))
                             .Returns(Task.FromResult(userInfo));

            // Act
            var result = await _sqlRepository.ReadAsync(id);

            // Assert
            Assert.Equal(userInfo, result);
            _mockDbConnection.Verify(db => db.QuerySingleOrDefaultAsync<UserInfo>(query, new { Id = id }, null, null, null), Times.Once);
        }

        [Fact]
        public async Task ReadAllAsync_ReturnsAllUserInfos()
        {
            // Arrange
            var userInfos = new List<UserInfo>
            {
                new UserInfo("testUser1", "testPass1", 1, 2),
                new UserInfo("testUser2", "testPass2", 3, 4)
            };
            var query = "SELECT * FROM [dbo].[User]";
            _mockDbConnection.Setup(db => db.QueryAsync<UserInfo>(query, null, null, null, null))
                             .Returns(Task.FromResult((IEnumerable<UserInfo>)userInfos));

            // Act
            var result = await _sqlRepository.ReadAllAsync();

            // Assert
            Assert.Equal(userInfos, result);
            _mockDbConnection.Verify(db => db.QueryAsync<UserInfo>(query, null, null, null, null), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesUserInfo()
        {
            // Arrange
            var userInfo = new UserInfo("testUser", "testPass", 1, 2);
            var query = @"UPDATE [dbo].[User] 
                          SET CurrentLevel = @CurrentLevel, Avatar = @Avatar 
                          WHERE Id = @Id";
            _mockDbConnection.Setup(db => db.ExecuteAsync(query, userInfo, null, null, null))
                             .Returns(Task.FromResult(1));

            // Act
            await _sqlRepository.UpdateAsync(userInfo);

            // Assert
            _mockDbConnection.Verify(db => db.ExecuteAsync(query, userInfo, null, null, null), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeletesUserInfo()
        {
            // Arrange
            var id = Guid.NewGuid();
            var query = "DELETE FROM [dbo].[User] WHERE Id = @Id";
            _mockDbConnection.Setup(db => db.ExecuteAsync(query, new { Id = id }, null, null, null))
                             .Returns(Task.FromResult(1));

            // Act
            await _sqlRepository.DeleteAsync(id);

            // Assert
            _mockDbConnection.Verify(db => db.ExecuteAsync(query, new { Id = id }, null, null, null), Times.Once);
        }
    }
}
