using Microsoft.EntityFrameworkCore;

namespace Service.Iletisim.Data
{
    public class IletisimContext : DbContext
    {
        public IletisimContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Entities.Iletisim> Iletisim { get; set; }
    }
}
