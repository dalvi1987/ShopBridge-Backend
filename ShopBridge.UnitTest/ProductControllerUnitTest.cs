using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Core;
using ShopBridge.Core.DTO;
using ShopBridge.Infra.Helpers;
using ShopBridge.Infra.Interface;
using ShopBridge.Infra.Repository;
using ShopBridge.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopBridge.UnitTest
{
    public class ProductControllerUnitTest
    {
        private readonly MapperConfiguration mockMapper;
        private readonly IMapper mapper;
        public ProductControllerUnitTest()
        {
            mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public async Task TestGetProductsAsync_ReturnProducts()
        {
            // Arrange
            var dbContext = DbContextMocker.GetAppDbContext(nameof(TestGetProductsAsync_ReturnProducts));
            var unitofWork = new UnitOfWork(dbContext);
            var controller = new ProductController(unitofWork, mapper);

            // Act
            var response = await controller.Get() as ObjectResult;
            var value = response.Value as IEnumerable<ProductDTO>;

            // Assert
            Assert.True(value.Count() > 0, "Expected to be greater than 0");
        }

        [Fact]
        public async Task TestGetProductsAsync_MatchResult()
        {
            // Arrange
            var dbContext = DbContextMocker.GetAppDbContext(nameof(TestGetProductsAsync_MatchResult));
            var unitofWork = new UnitOfWork(dbContext);
            var controller = new ProductController(unitofWork, mapper);

            // Act
            var response = await controller.Get() as ObjectResult;
            var value = response.Value as IEnumerable<ProductDTO>;

            // Assert
            Assert.Equal("Test Product 1", value.First(x => x.Id == 1).Name);
            Assert.Equal("Test Product 2", value.First(x => x.Id == 2).Name);
        }

        [Fact]
        public async Task TestGetProductAsync_MatchResult()
        {
            // Arrange
            var dbContext = DbContextMocker.GetAppDbContext(nameof(TestGetProductAsync_MatchResult));
            var unitofWork = new UnitOfWork(dbContext);

            var controller = new ProductController(unitofWork, mapper);
            var id = 1;

            // Act
            var response = await controller.Get(id) as ObjectResult;
            var value = response.Value as ProductDTO;

            // Assert
            Assert.True(value.Name == "Test Product 1");
        }

        [Fact]
        public async Task TestGetProductAsync_NotFound()
        {
            // Arrange
            var dbContext = DbContextMocker.GetAppDbContext(nameof(TestGetProductAsync_NotFound));
            var unitofWork = new UnitOfWork(dbContext);

            var controller = new ProductController(unitofWork, mapper);
            var id = 5;

            // Act
            var response = await controller.Get(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(response);
        }

        [Fact]
        public async Task TestDeleteProductAsync_OkResult()
        {
            // Arrange
            var dbContext = DbContextMocker.GetAppDbContext(nameof(TestDeleteProductAsync_OkResult));
            var unitofWork = new UnitOfWork(dbContext);

            var controller = new ProductController(unitofWork, mapper);
            var id = 1;

            // Act
            var response = await controller.Delete(id);

            //Asert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public async Task TestDeleteProductAsync_NotFoundResult()
        {
            // Arrange
            var dbContext = DbContextMocker.GetAppDbContext(nameof(TestDeleteProductAsync_NotFoundResult));
            var unitofWork = new UnitOfWork(dbContext);

            var controller = new ProductController(unitofWork, mapper);
            var id = 5;

            // Act
            var response = await controller.Delete(id);

            //Asert
            Assert.IsType<NotFoundObjectResult>(response);
        }

        [Fact]
        public async Task TestPostProductAsync_CreatedItem()
        {
            // Arrange
            var dbContext = DbContextMocker.GetAppDbContext(nameof(TestPostProductAsync_CreatedItem));
            var unitofWork = new UnitOfWork(dbContext);

            var controller = new ProductController(unitofWork, mapper);
            ProductDTO productDto = new ProductDTO
            {
                Name = "Test Product 4",
                Description = "Test Product Description 4",
                Discontinued = false,
                UnitId = 2,
                UnitPrice = 40.25M,
                UnitsInStock = 23
            };

            // Act
            var response = await controller.Post(productDto);

            //Asert
            Assert.IsType<CreatedAtActionResult>(response);
        }

        [Fact]
        public async Task TestPostProductAsync_BadRequest()
        {
            // Arrange
            var dbContext = DbContextMocker.GetAppDbContext(nameof(TestPostProductAsync_BadRequest));
            var unitofWork = new UnitOfWork(dbContext);

            var controller = new ProductController(unitofWork, mapper);
            ProductDTO productDto = null;

            // Act
            var response = await controller.Post(productDto);

            //Asert
            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public async Task TestPutProductAsync_OkResult()
        {
            // Arrange
            var dbContext = DbContextMocker.GetAppDbContext(nameof(TestPutProductAsync_OkResult));
            var unitofWork = new UnitOfWork(dbContext);

            var controller = new ProductController(unitofWork, mapper);
            var id = 3;
            ProductDTO productDto = new ProductDTO
            {
                Id = 3,
                Name = "Test Product 3 Update",
                Description = "Test Product Description 3 Update",
                Discontinued = false,
                UnitId = 2,
                UnitPrice = 40.25M,
                UnitsInStock = 23
            };

            // Act
            var response = await controller.Put(id, productDto);

            //Asert
            Assert.IsType<OkResult>(response);
        }


    }
}
