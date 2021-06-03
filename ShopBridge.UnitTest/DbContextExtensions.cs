using ShopBridge.Core;
using ShopBridge.Infra;

namespace ShopBridge.UnitTest
{
    public static class DbContextExtensions
    {
        public static void Seed(this AppDbContext dbContext)
        {
            dbContext.Units.Add(new Unit
            {
                Id = 1,
                UnitName = "each"
            });

            dbContext.Units.Add(new Unit
            {
                Id = 2,
                UnitName = "kilogram"
            });

            dbContext.Units.Add(new Unit
            {
                Id = 3,
                UnitName = "piece"
            });


            dbContext.Products.Add(new Product
            {
                Id = 1,
                Name = "Test Product 1",
                Description = "Test Product Descripion 1",
                UnitPrice = 10.00M,
                UnitsInStock = 10,
                Discontinued = false,
                UnitId = 1
            });

            dbContext.Products.Add(new Product
            {
                Id = 2,
                Name = "Test Product 2",
                Description = "Test Product Descripion 2",
                UnitPrice = 20.00M,
                UnitsInStock = 20,
                Discontinued = false,
                UnitId = 2
            });

            dbContext.Products.Add(new Product
            {
                Id = 3,
                Name = "Test Product 3",
                Description = "Test Product Descripion 3",
                UnitPrice = 30.00M,
                UnitsInStock = 30,
                Discontinued = false,
                UnitId = 3
            });

            dbContext.SaveChanges();
        }
    }
}
