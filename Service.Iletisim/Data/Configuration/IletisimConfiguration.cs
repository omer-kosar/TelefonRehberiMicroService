using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Iletisim.Data.Configuration
{
    public class IletisimConfiguration : IEntityTypeConfiguration<Iletisim.Entities.Iletisim>
    {
        public void Configure(EntityTypeBuilder<Entities.Iletisim> builder)
        {
            builder.Property(e => e.Icerik)
              .HasMaxLength(50)
              .IsRequired(true);
        }
    }
}
