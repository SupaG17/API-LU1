using Dapper;
using LU1_project.Models;
using LU1_project.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TestProject2
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
        public async Task InsertAsync_ShouldInsertUserInfo()
        {
            // Arrange
            var userInfo = new UserInfo("testUser", "testPass", 1, 1) { Id = Guid.NewGuid() };
            _mockDbConnection.Setup(db => db.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                             .ReturnsAsync(1);

            // Act
            var result = await _sqlRepository.InsertAsync(userInfo);

            // Assert
            Assert.Equals(userInfo, result);
            _mockDbConnection.Verify(db => db.ExecuteAsync(It.IsAny<string>(), userInfo, null, null, null), Times.Once);
        }

        [Fact]
        public async Task ReadAsync_ById_ShouldReturnUserInfo()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userInfo = new UserInfo("testUser", "testPass", 1, 1) { Id = userId };
            _mockDbConnection.Setup(db => db.QuerySingleOrDefaultAsync<UserInfo>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                             .ReturnsAsync(userInfo);

            // Act
            var result = await _sqlRepository.ReadAsync(userId);

            // Assert
            Assert.Equals(userInfo, result);
            _mockDbConnection.Verify(db => db.QuerySingleOrDefaultAsync<UserInfo>(It.IsAny<string>(), new { Id = userId }, null, null, null), Times.Once);
        }

        [Fact]
        public async Task ReadAsync_ByUserName_ShouldReturnUserInfo()
        {
            // Arrange
            var userName = "testUser";
            var userInfo = new UserInfo(userName, "testPass", 1, 1) { Id = Guid.NewGuid() };
            _mockDbConnection.Setup(db => db.QuerySingleOrDefaultAsync<UserInfo>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                             .ReturnsAsync(userInfo);

            // Act
            var result = await _sqlRepository.ReadAsync(userName);

            // Assert
            Assert.Equals(userInfo, result);
            _mockDbConnection.Verify(db => db.QuerySingleOrDefaultAsync<UserInfo>(It.IsAny<string>(), new { UserName = userName }, null, null, null), Times.Once);
        }

        [Fact]
        public async Task ReadAllAsync_ShouldReturnAllUserInfos()
        {
            // Arrange
            var userInfos = new List<UserInfo>
            {
                new UserInfo("testUser1", "testPass1", 1, 1) { Id = Guid.NewGuid() },
                new UserInfo("testUser2", "testPass2", 2, 2) { Id = Guid.NewGuid() }
            };
            _mockDbConnection.Setup(db => db.QueryAsync<UserInfo>(It.IsAny<string>(), null, null, null, null))
                             .ReturnsAsync(userInfos);

            // Act
            var result = await _sqlRepository.ReadAllAsync();

            // Assert
            Assert.Equals(userInfos, result);
            _mockDbConnection.Verify(db => db.QueryAsync<UserInfo>(It.IsAny<string>(), null, null, null, null), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateUserInfo()
        {
            // Arrange
            var userInfo = new UserInfo("testUser", "testPass", 1, 1) { Id = Guid.NewGuid() };
            _mockDbConnection.Setup(db => db.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                             .ReturnsAsync(1);

            // Act
            await _sqlRepository.UpdateAsync(userInfo);

            // Assert
            _mockDbConnection.Verify(db => db.ExecuteAsync(It.IsAny<string>(), userInfo, null, null, null), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteUserInfo()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockDbConnection.Setup(db => db.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                             .ReturnsAsync(1);

            // Act
            await _sqlRepository.DeleteAsync(userId);

            // Assert
            _mockDbConnection.Verify(db => db.ExecuteAsync(It.IsAny<string>(), new { Id = userId }, null, null, null), Times.Once);
        }
    }
}
