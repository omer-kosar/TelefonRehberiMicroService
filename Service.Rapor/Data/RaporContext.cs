using Microsoft.EntityFrameworkCore;
using Service.Rapor.Data.Configuration;
using Service.Rapor.Entities;

namespace Service.Kisi.Data
{
    public class RaporContext : DbContext
    {
        public RaporContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Rapor.Entities.Rapor> Rapor { get; set; }
        public DbSet<Rapor.Entities.RaporBilgi> RaporBilgi { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RaporConfiguration());
            modelBuilder.ApplyConfiguration(new RaporBilgiConfiguration());
        }
    }
}
