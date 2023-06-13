using Microsoft.EntityFrameworkCore;
using Service.Kisi.Data.Configuration;

namespace Service.Kisi.Data
{
    public class KisiContext : DbContext
    {
        public KisiContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Entities.Kisi> Kisi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new KisiConfiguration());
        }
    }
}
