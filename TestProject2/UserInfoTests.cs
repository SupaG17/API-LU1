using Microsoft.VisualStudio.TestTools.UnitTesting;
using LU1_project.Models;
using System;

namespace LU1_project.Tests
{
    [TestClass]
    public class UserInfoTests
    {
        [TestMethod]
        public void UserInfo_Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            string expectedUserName = "testUser";
            string expectedPassword = "securePassword123";
            int? expectedLevel = 5;
            int? expectedAvatar = 2;

            // Act
            var userInfo = new UserInfo(expectedUserName, expectedPassword, expectedLevel, expectedAvatar);

            // Assert
            Assert.AreEqual(expectedUserName, userInfo.UserName);
            Assert.AreEqual(expectedPassword, userInfo.PassWord);
            Assert.AreEqual(expectedLevel, userInfo.CurrentLevel);
            Assert.AreEqual(expectedAvatar, userInfo.Avatar);
            Assert.AreNotEqual(Guid.Empty, userInfo.Id);
        }
    }
}