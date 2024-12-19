using dotnet_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductsNamespace;
using System;

namespace YourAppNamespace
{
    public static class DatabaseInitializer
    {
        public static void InitializeDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();

            // Verifica ou cria o banco de dados
            if (dbContext.Database.EnsureCreated())
            {
                Console.WriteLine("Banco de dados criado.");
                SeedData(dbContext);
            }
            else
            {
                Console.WriteLine("Banco de dados já existe.");
            }
        }

        private static void SeedData(ProductsDbContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.Add(new Product { Name = "Exemplo de produto", CategoryId=1, IsDeleted=false,Category="teste category", Supplier="supplier test", SupplierId=1 });
                context.SaveChanges();
            }
            //TODO POPULATE CATEGORY AND SUPPLIER
        }
    }
}
