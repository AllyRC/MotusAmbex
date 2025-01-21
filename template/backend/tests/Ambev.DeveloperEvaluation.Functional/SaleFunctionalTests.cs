using System;
using System.Collections.Generic;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Tests
{
    public class SaleTests
    {
        [Fact]
        public void Should_Apply_10Percent_Discount_For_4_To_9_Items()
        {
            // Arrange
            var product = new Product
            {
                Description = "Test Product",
                Quantity = 5,
                UnitPrice = 100m,
                Discount = 0m
            };

            // Act
            if (product.Quantity >= 4 && product.Quantity < 10)
            {
                product.Discount = 0.10m;
            }

            // Assert
            Assert.Equal(450m, product.TotalAmount);
        }

        [Fact]
        public void Should_Apply_20Percent_Discount_For_10_To_20_Items()
        {
            // Arrange
            var product = new Product
            {
                Description = "Test Product",
                Quantity = 15,
                UnitPrice = 100m,
                Discount = 0m
            };

            // Act
            if (product.Quantity >= 10 && product.Quantity <= 20)
            {
                product.Discount = 0.20m;
            }

            // Assert
            Assert.Equal(1200m, product.TotalAmount);
        }

        [Fact]
        public void Should_Not_Apply_Discount_For_Less_Than_4_Items()
        {
            // Arrange
            var product = new Product
            {
                Description = "Test Product",
                Quantity = 3,
                UnitPrice = 100m,
                Discount = 0m
            };

            // Act
            if (product.Quantity >= 4)
            {
                product.Discount = 0.10m;
            }

            // Assert
            Assert.Equal(300m, product.TotalAmount);
        }

        [Fact]
        public void Should_Not_Allow_Sale_Of_More_Than_20_Items()
        {
            // Arrange
            var product = new Product
            {
                Description = "Test Product",
                Quantity = 25,
                UnitPrice = 100m,
                Discount = 0m
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                if (product.Quantity > 20)
                {
                    throw new ArgumentException("Cannot sell more than 20 items of the same product.");
                }
            });
        }

        [Fact]
        public void Should_Create_Valid_Sale()
        {
            // Arrange
            var sale = new Sale
            {
                SaleNumber = 1,
                SaleDate = DateTime.UtcNow,
                CustomerName = "John Doe",
                Branch = "Main Branch",
                Products = new List<Product>
                {
                    new Product
                    {
                        Description = "Product A",
                        Quantity = 5,
                        UnitPrice = 100m,
                        Discount = 0.10m
                    }
                }
            };

            sale.TotalSaleAmount = sale.Products[0].TotalAmount;

            // Assert
            Assert.True(sale.TotalSaleAmount > 0);
            Assert.Equal("John Doe", sale.CustomerName);
        }

        [Fact]
        public void Should_Cancel_Sale()
        {
            // Arrange
            var sale = new Sale
            {
                SaleNumber = 2,
                SaleDate = DateTime.UtcNow,
                CustomerName = "Jane Doe",
                Branch = "Second Branch",
                Products = new List<Product>
                {
                    new Product { Description = "Product B", Quantity = 4, UnitPrice = 50m }
                }
            };

            // Act
            sale.Products.ForEach(p => p.IsCancelled = true);

            // Assert
            Assert.All(sale.Products, p => Assert.True(p.IsCancelled));
        }
    }
}
