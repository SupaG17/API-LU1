using System;
using LU1_project.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LU1_project.Tests
{
    [TestClass]
    public class UserInfoTests
    {
        [TestMethod]
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
            Assert.AreEqual(userName, userInfo.UserName);
            Assert.AreEqual(passWord, userInfo.PassWord);
            Assert.AreEqual(currentLevel, userInfo.CurrentLevel);
            Assert.AreEqual(avatar, userInfo.Avatar);
            Assert.AreNotEqual(Guid.Empty, userInfo.Id);
        }
    }
}
