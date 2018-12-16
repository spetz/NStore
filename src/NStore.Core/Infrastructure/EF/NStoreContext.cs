using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NStore.Core.Domain;
using NStore.Core.Infrastructure.EF.Configurations;

namespace NStore.Core.Infrastructure.EF
{
    public class NStoreContext : DbContext
    {
        private readonly IOptions<SqlOptions> _sqlOptions;
        
        public DbSet<Product> Products { get; set; }

        public NStoreContext(IOptions<SqlOptions> sqlOptions)
        {
            _sqlOptions = sqlOptions;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            if (_sqlOptions.Value.InMemory)
            {
                optionsBuilder.UseInMemoryDatabase(_sqlOptions.Value.Database);
                
                return;
            }

            optionsBuilder.UseSqlServer(_sqlOptions.Value.ConnectionString,
                o => o.MigrationsAssembly("NStore.Web"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}