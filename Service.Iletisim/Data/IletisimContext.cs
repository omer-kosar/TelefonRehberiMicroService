using Microsoft.EntityFrameworkCore;
using Service.Iletisim.Data.Configuration;

namespace Service.Iletisim.Data
{
    public class IletisimContext : DbContext
    {
        public IletisimContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Entities.Iletisim> Iletisim { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new IletisimConfiguration());
        }
    }
}
