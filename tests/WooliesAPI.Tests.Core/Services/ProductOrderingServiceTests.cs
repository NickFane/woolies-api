using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WooliesAPI.Core.Services;
using WooliesAPI.Domain.Entities;

namespace WooliesAPI.Tests.Core.Services
{
    public class ProductOrderingServiceTests
    {
        private IProductOrderingService _target;
        private Mock<IPopularProductService> _mockedProductPopularityService;

        private List<Product> _products;

        private const string productAName = "testA";
        private const string productBName = "testB";
        private const string productCName = "testC";

        [SetUp]
        public void SetUp()
        {
            _mockedProductPopularityService = new Mock<IPopularProductService>();
            _target = new ProductOrderingService(_mockedProductPopularityService.Object);

            var productA = new Product() { Name = productAName, Price = 3, Quantity = 0 };
            var productB = new Product() { Name = productBName, Price = 1, Quantity = 0 };
            var productC = new Product() { Name = productCName, Price = 5, Quantity = 0 };
            _products = new List<Product>() { productA, productB, productC };

        }


        // Test should pass even with no logic
        // Just to ensure any potential changes alert via broken tests
        [Test]
        public void ReturnsProducts_OrderedByAscending()
        {
            // Already Arranged

            // Act
            var result = _target.OrderProductsAsync(_products, Domain.Enums.SortOption.Ascending).Result.ToList();

            // Assert
            Assert.IsTrue(result.First().Name == productAName);
            Assert.IsTrue(result.Last().Name == productCName);
        }

        [Test]
        public void ReturnsProducts_OrderedByDescending()
        {
            // Already Arranged

            // Act
            var result = _target.OrderProductsAsync(_products, Domain.Enums.SortOption.Descending).Result.ToList();

            // Assert
            Assert.IsTrue(result.First().Name == productCName);
            Assert.IsTrue(result.Last().Name == productAName);
        }

        [Test]
        public void ReturnsProducts_OrderedByLow()
        {
            // Already Arranged

            // Act
            var result = _target.OrderProductsAsync(_products, Domain.Enums.SortOption.Low).Result.ToList();

            // Assert
            Assert.IsTrue(result.First().Name == productBName);
            Assert.IsTrue(result.Last().Name == productCName);
        }

        [Test]
        public void ReturnsProducts_OrderedByHigh()
        {
            // Already Arranged

            // Act
            var result = _target.OrderProductsAsync(_products, Domain.Enums.SortOption.High).Result.ToList();

            // Assert
            Assert.IsTrue(result.First().Name == productCName);
            Assert.IsTrue(result.Last().Name == productBName);
        }

        [Test]
        public void ReturnsProducts_OrderedByRecommended_WithDefinedPopularity()
        {
            // Arrange
            var productPopularityA = new ProductPopularity() { ProductName = productAName, PopularityRank = 2 };
            var productPopularityB = new ProductPopularity() { ProductName = productBName, PopularityRank = 0 };
            var productPopularityC = new ProductPopularity() { ProductName = productCName, PopularityRank = 1 };

            _mockedProductPopularityService.Setup(s => s.GetProductPopularityAsync())
                .ReturnsAsync(new List<ProductPopularity>() { productPopularityA, productPopularityB, productPopularityC });

            // Act
            var result = _target.OrderProductsAsync(_products, Domain.Enums.SortOption.Recommended).Result.ToList();

            // Assert
            Assert.IsTrue(result.First().Name == productBName);
            Assert.IsTrue(result.Last().Name == productAName);
        }
    }
}
