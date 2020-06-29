using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Profiles;
using Ecommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Api.Products.Tests
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task GetProductsReturnAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnAllProducts))
                .Options;
            var dbcontext = new ProductsDbContext(options);

            CreateProducts(dbcontext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbcontext, null, mapper);
            var products = await productsProvider.GetProductsAsync();

            Assert.True(products.IsSuccess);
            Assert.True(products.products.Any());
            Assert.Null(products.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnProductUsingValidId))
                .Options;
            var dbcontext = new ProductsDbContext(options);

            CreateProducts(dbcontext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbcontext, null, mapper);
            var product = await productsProvider.GetProductAsync(1);

            Assert.True(product.IsSuccess);
            Assert.NotNull(product.product);
            Assert.True(product.product.Id == 1);
            Assert.Null(product.ErrorMessage);
        }


        [Fact]
        public async Task GetProductsReturnProductUsingInValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnProductUsingInValidId))
                .Options;
            var dbcontext = new ProductsDbContext(options);

            CreateProducts(dbcontext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbcontext, null, mapper);
            var product = await productsProvider.GetProductAsync(-1);

            Assert.False(product.IsSuccess);
            Assert.Null(product.product);
            Assert.NotNull(product.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext dbcontext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbcontext.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = 10, 
                    Price = (decimal) (i*3.14)
                });
            }

            dbcontext.SaveChanges();
        }
    }
}
