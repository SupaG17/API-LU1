// UserInfoTests.cs
using System;
using LU1_project.Models;
using Xunit;
using Assert = Xunit.Assert;


namespace LU1_project.Tests
{
    public class UserInfoTests
    {
        [Fact]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            // Arrange
            var userName = "testUser";
            var passWord = "testPass";
            int? currentLevel = 1;
            int? avatar = 2;

            // Act
            var userInfo = new UserInfo(userName, passWord, currentLevel, avatar);

            // Assert
            Assert.Equal(userName, userInfo.UserName);
            Assert.Equal(passWord, userInfo.PassWord);
            Assert.Equal(currentLevel, userInfo.CurrentLevel);
            Assert.Equal(avatar, userInfo.Avatar);
            Assert.NotEqual(Guid.Empty, userInfo.Id);
        }
    }
}
