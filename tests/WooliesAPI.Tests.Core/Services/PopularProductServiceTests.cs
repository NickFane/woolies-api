using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesAPI.Core.Services;
using WooliesAPI.Domain.Entities;
using WooliesAPI.Persistence.Api;

namespace WooliesAPI.Tests.Core.Services
{
    public class PopularProductServiceTests
    {
        private IPopularProductService _target;
        private Mock<IResourceApi> _resourceApiMock;

        [SetUp]
        public void SetUp()
        {
            _resourceApiMock = new Mock<IResourceApi>();
            _target = new PopularProductService(_resourceApiMock.Object);
        }

        [Test]
        public void ReturnsPopularProducts_Successfully()
        {
            // Arrange
            SetupShopperHistory();

            // Act
            var result = _target.GetProductPopularityAsync().Result.ToList();

            // Assert
            Assert.IsTrue(result.Single(s => s.ProductName == "testA").PopularityRank == 0);
            Assert.IsTrue(result.Single(s => s.ProductName == "testB").PopularityRank == 1);
            Assert.IsTrue(result.Single(s => s.ProductName == "testC").PopularityRank == 2);
        }

        // This test is based on logic that breaks the challenge.
        // However as we have no control on the ordering of products we get from resource API
        // I believe a secondary sort is required to maintain consistency
        [Test]
        [Ignore("This fails the challenge")]
        public void ReturnsEqualPopularProducts_NameSorted()
        {
            // Arrange
            SetupEqualPopularityShoppingHistory();

            // Act
            var result = _target.GetProductPopularityAsync().Result.ToList();

            // Assert
            Assert.IsTrue(result.Single(s => s.ProductName == "testA").PopularityRank == 0);
            Assert.IsTrue(result.Single(s => s.ProductName == "testB").PopularityRank == 1);
            Assert.IsTrue(result.Single(s => s.ProductName == "testC").PopularityRank == 2);
        }

        private void SetupShopperHistory()
        {
            var shopperHistory1 = new ShopperHistory() { CustomerId = 1, Products = new List<Product>() { new Product() { Name = "testA", Quantity = 5 } } };
            var shopperHistory2 = new ShopperHistory() { CustomerId = 2, Products = new List<Product>() { new Product() { Name = "testB", Quantity = 2 } } };
            var shopperHistory3 = new ShopperHistory() { CustomerId = 3, Products = new List<Product>() { new Product() { Name = "testC", Quantity = 1 } } };

            var defaultShopperHistory = new List<ShopperHistory>() { shopperHistory1, shopperHistory2, shopperHistory3 };
            _resourceApiMock.Setup(s => s.GetShopperHistory()).ReturnsAsync(defaultShopperHistory);
        }

        private void SetupEqualPopularityShoppingHistory()
        {
            var shopperHistory1 = new ShopperHistory() { CustomerId = 1, Products = new List<Product>() { new Product() { Name = "testC", Quantity = 1 } } };
            var shopperHistory2 = new ShopperHistory() { CustomerId = 2, Products = new List<Product>() { new Product() { Name = "testB", Quantity = 1 } } };
            var shopperHistory3 = new ShopperHistory() { CustomerId = 3, Products = new List<Product>() { new Product() { Name = "testA", Quantity = 1 } } };

            var equalPopularityShoppingHistory = new List<ShopperHistory>() { shopperHistory1, shopperHistory2, shopperHistory3 };
            _resourceApiMock.Setup(s => s.GetShopperHistory()).ReturnsAsync(equalPopularityShoppingHistory);
        }
    }
}
