using Microsoft.EntityFrameworkCore;
using Customer.Domain;
using Customer.Persistence.Database.Configuration;

namespace Customer.Persistence.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Customer");

            ModelConfig(modelBuilder);
        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new ClientConfiguration(modelBuilder.Entity<Client>());
        }
    }
}
