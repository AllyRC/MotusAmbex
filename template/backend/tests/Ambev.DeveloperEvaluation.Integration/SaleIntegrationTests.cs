using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Tests.Integration
{
    public class SaleIntegrationTests
    {
        private readonly HttpClient _client;

        public SaleIntegrationTests()
        {
            _client = new HttpClient { BaseAddress = new Uri("https://localhost:5001/api/") };
        }

        [Fact]
        public async Task Should_Create_Sale_Successfully()
        {
            // Arrange
            var sale = new Sale
            {
                SaleNumber = 1001,
                SaleDate = DateTime.UtcNow,
                CustomerName = "Test Customer",
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

            var jsonContent = new StringContent(JsonSerializer.Serialize(sale), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("sales", jsonContent);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Should_Get_Sale_By_Id()
        {
            // Arrange
            int saleId = 1001;

            // Act
            var response = await _client.GetAsync($"sales/{saleId}");
            var responseData = await response.Content.ReadAsStringAsync();
            var sale = JsonSerializer.Deserialize<Sale>(responseData);

            // Assert
            Assert.NotNull(sale);
            Assert.Equal(1001, sale.SaleNumber);
        }

        [Fact]
        public async Task Should_Cancel_Sale()
        {
            // Arrange
            int saleId = 1001;

            // Act
            var response = await _client.DeleteAsync($"sales/{saleId}");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
