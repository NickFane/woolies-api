using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WooliesAPI.Domain.Entities;
using WooliesAPI.Persistence.Db;

namespace WooliesAPI.Tests.Persistence.Db
{
    public class WooliesApiDbContextTests
    {
        private IDbContext _target;

        [SetUp]
        public void SetUp()
        {
            _target = new WooliesApiDbContext(new List<User>()
            {
                new User(){Name = "user1", Token = "1"},
                new User(){Name = "user2", Token = "2"},
                new User(){Name = "user3", Token = "3"},
            });
        }

        [TestCase("1", "user1")]
        [TestCase("2", "user2")]
        public async Task GetUser_ReturnsUser(string userToken, string expectedName)
        {
            // Arrange
            var expectedUser = new User() { Name = expectedName, Token = userToken };

            // Act
            var result = await _target.GetUserAsync(expectedUser.Token);

            // Assert
            Assert.AreEqual(expectedUser.Name, result.Name);
            Assert.AreEqual(expectedUser.Token, result.Token);
        }

        [Test]
        public async Task GetUserWithoutToken_DoesNotThrow()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                await _target.GetUserAsync();
            });
        }

        [Test]
        public async Task GetUserWithoutToken_ReturnsFirstUser()
        {
            // Arrange
            var expectedUser = new User() { Name = "user1", Token = "1" };

            // Act
            var result = await _target.GetUserAsync(null);

            // Assert

            Assert.AreEqual(expectedUser.Name, result.Name);
            Assert.AreEqual(expectedUser.Token, result.Token);
        }
    }
}
