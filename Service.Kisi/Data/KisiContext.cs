using Microsoft.EntityFrameworkCore;

namespace Service.Kisi.Data
{
    public class KisiContext : DbContext
    {
        public KisiContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Entities.Kisi> Kisi { get; set; }
    }
}
