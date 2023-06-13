using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Kisi.Data.Configuration
{
    public class KisiConfiguration : IEntityTypeConfiguration<Entities.Kisi>
    {
        public void Configure(EntityTypeBuilder<Entities.Kisi> builder)
        {
            builder.Property(e => e.Ad)
            .HasMaxLength(50)
            .IsRequired(true);
            builder.Property(e => e.Soyad)
               .HasMaxLength(50)
               .IsRequired(true);
            builder.Property(e => e.Firma)
               .HasMaxLength(100)
               .IsRequired(true);
        }
    }
}
