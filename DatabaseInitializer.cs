using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace YourAppNamespace
{
    public static class DatabaseInitializer
    {
        public static void InitializeDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<YourDbContext>();

            // Verifica ou cria o banco de dados
            if (dbContext.Database.EnsureCreated())
            {
                Console.WriteLine("Banco de dados criado.");
                SeedData(dbContext); // Popula os dados iniciais
            }
            else
            {
                Console.WriteLine("Banco de dados já existe.");
            }
        }

        private static void SeedData(YourDbContext context)
        {
            // Adicione dados iniciais
            if (!context.YourEntities.Any())
            {
                context.YourEntities.Add(new YourEntity { Name = "Exemplo" });
                context.SaveChanges();
            }
        }
    }
}
