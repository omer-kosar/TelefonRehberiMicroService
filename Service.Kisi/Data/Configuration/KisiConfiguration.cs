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

            builder.HasData(new Entities.Kisi
            {
                Id = new Guid("856b39fb-d802-4574-a0b4-872a12589c59"),
                Ad = "John",
                Soyad = "Doe",
                Firma = "ABC Corporation"

            },
          new Entities.Kisi
          {
              Id = new Guid("927f71b9-fef6-44ce-b6d8-a8ee86df89e0"),
              Ad = "Jane",
              Soyad = "Smith",
              Firma = " XYZ Ltd."

          },
          new Entities.Kisi
          {
              Id = new Guid("a5ce0ec5-1c1a-4a15-86f9-cdde439a92a2"),
              Ad = "Michael",
              Soyad = "Johnson",
              Firma = "QWERTY Holdings"

          },
          new Entities.Kisi
          {
              Id = new Guid("e0207c71-f788-4067-8ebf-3a48e7b6966e"),
              Ad = "Emily",
              Soyad = "Brown",
              Firma = "XYZ Ltd."

          });
        }
    }
}
