using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Rapor.Enums;

namespace Service.Rapor.Data.Configuration
{
    public class RaporConfiguration : IEntityTypeConfiguration<Entities.Rapor>
    {
        public void Configure(EntityTypeBuilder<Entities.Rapor> builder)
        {
            builder.Property(e => e.TalepEdildigiTarih)
               .IsRequired(true);
            builder.Property(e => e.Durum)
               .IsRequired(true);
            builder.HasMany<Entities.RaporBilgi>(raporBilgi => raporBilgi.RaporBilgileri).WithOne(r => r.Rapor).HasForeignKey(rb => rb.RaporId).OnDelete(deleteBehavior: DeleteBehavior.Cascade);
            builder.HasData(new Entities.Rapor
            {
                Id = new Guid("d6245fe2-0947-11ee-b208-54e1ad72c6a1"),
                Durum = (int)RaporDurum.Tamamlandi,
                TalepEdildigiTarih = DateTime.Now
            }, new Entities.Rapor
            {
                Id = new Guid("e50ef18e-0947-11ee-b20b-54e1ad72c6a1"),
                Durum = (int)RaporDurum.Tamamlandi,
                TalepEdildigiTarih = DateTime.Now
            }
            );
        }
    }
}
