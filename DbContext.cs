using Microsoft.EntityFrameworkCore;

namespace YourAppNamespace
{
    public class YourDbContext : DbContext
    {
        public YourDbContext(DbContextOptions<YourDbContext> options) : base(options) { }

        // Defina suas tabelas
        public DbSet<YourEntity> YourEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configurações adicionais, se necessário
        }
    }

    public class YourEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
