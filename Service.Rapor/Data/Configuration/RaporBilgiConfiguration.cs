using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Service.Rapor.Data.Configuration
{
    public class RaporBilgiConfiguration : IEntityTypeConfiguration<Entities.RaporBilgi>
    {
        public void Configure(EntityTypeBuilder<Entities.RaporBilgi> builder)
        {
            builder.Property(e => e.KonumBilgisi)
                .HasMaxLength(50)
                .IsRequired(true);
            builder.Property(e => e.KisiSayisi).IsRequired(true);
            builder.Property(e => e.KonumBilgisi).IsRequired(true);
            builder.HasData(
                new Entities.RaporBilgi
                {
                    Id = new Guid("07f4b594-0948-11ee-b20c-54e1ad72c6a1"),
                    RaporId = new Guid("d6245fe2-0947-11ee-b208-54e1ad72c6a1"),
                    KisiSayisi = 2,
                    TelefonNumarasiSayisi = 3,
                    KonumBilgisi = "Ankara"
                },
                new Entities.RaporBilgi
                {
                    Id = new Guid("7ea050d6-0948-11ee-b20d-54e1ad72c6a1"),
                    RaporId = new Guid("e50ef18e-0947-11ee-b20b-54e1ad72c6a1"),
                    KonumBilgisi = "Mersin",
                    KisiSayisi = 0,
                    TelefonNumarasiSayisi = 0,
                });
        }
    }
}
