using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.DataContext.SeedData
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context,ILoggerFactory loggerFactory)
        {
            try
            {
                if(!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/DataContext/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var brand in brands)
                    {
                        context.ProductBrands.Add(brand);
                    }
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ProductBrands ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ProductBrands OFF;");
                }

                if (!context.ProductTypes.Any())
                {
                    var productTypesData = File.ReadAllText("../Infrastructure/DataContext/SeedData/types.json");
                    var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);
                    foreach (var productType in productTypes)
                    {
                        context.ProductTypes.Add(productType);
                    }
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ProductTypes ON;");
                    await context.SaveChangesAsync();
                    context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ProductTypes ON;");
                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/DataContext/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var product in products)
                    {
                        context.Products.Add(product);
                    }
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
