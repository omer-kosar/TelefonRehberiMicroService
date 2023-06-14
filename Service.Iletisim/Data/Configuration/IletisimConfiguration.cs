using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Iletisim.Enums;

namespace Service.Iletisim.Data.Configuration
{
    public class IletisimConfiguration : IEntityTypeConfiguration<Iletisim.Entities.Iletisim>
    {
        public void Configure(EntityTypeBuilder<Entities.Iletisim> builder)
        {
            builder.Property(e => e.Icerik)
              .HasMaxLength(50)
              .IsRequired(true);

            builder.HasData(new Entities.Iletisim
            {
                KisiId = new Guid("856b39fb-d802-4574-a0b4-872a12589c59"),
                IletisimType = (int)IletisimType.Telefon,
                Icerik = "05383941232"
            },
           new Entities.Iletisim
           {
               KisiId = new Guid("856b39fb-d802-4574-a0b4-872a12589c59"),
               IletisimType = (int)IletisimType.EPosta,
               Icerik = "abc@abc.com"
           },
           new Entities.Iletisim
           {
               KisiId = new Guid("856b39fb-d802-4574-a0b4-872a12589c59"),
               IletisimType = (int)IletisimType.Konum,
               Icerik = "Ankara"
           },
              new Entities.Iletisim
              {
                  KisiId = new Guid("927f71b9-fef6-44ce-b6d8-a8ee86df89e0"),
                  IletisimType = (int)IletisimType.Konum,
                  Icerik = "Ankara"
              },
             new Entities.Iletisim
             {
                 KisiId = new Guid("927f71b9-fef6-44ce-b6d8-a8ee86df89e0"),
                 IletisimType = (int)IletisimType.Telefon,
                 Icerik = "05383946677"
             },

              new Entities.Iletisim
              {
                  KisiId = new Guid("927f71b9-fef6-44ce-b6d8-a8ee86df89e0"),
                  IletisimType = (int)IletisimType.Telefon,
                  Icerik = "05383948899"
              }
           );
        }
    }
}
