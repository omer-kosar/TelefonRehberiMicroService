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
                Id=new Guid("e85e3cf0-974e-4bc5-9dee-7d47094d039e"),
                KisiId = new Guid("856b39fb-d802-4574-a0b4-872a12589c59"),
                IletisimType = (int)IletisimType.Telefon,
                Icerik = "05383941232"
            },
           new Entities.Iletisim
           {
               Id = new Guid("6eb946fb-e1c4-4c02-a8c6-c4a9423b9fa5"),
               KisiId = new Guid("856b39fb-d802-4574-a0b4-872a12589c59"),
               IletisimType = (int)IletisimType.EPosta,
               Icerik = "abc@abc.com"
           },
           new Entities.Iletisim
           {
               Id = new Guid("c042ae87-1a13-4cd2-a281-fa7f7b579118"),
               KisiId = new Guid("856b39fb-d802-4574-a0b4-872a12589c59"),
               IletisimType = (int)IletisimType.Konum,
               Icerik = "Ankara"
           },
              new Entities.Iletisim
              {
                  Id = new Guid("1f1dfa36-bbde-4fd3-87bd-4c0aaed711b1"),
                  KisiId = new Guid("927f71b9-fef6-44ce-b6d8-a8ee86df89e0"),
                  IletisimType = (int)IletisimType.Konum,
                  Icerik = "Ankara"
              },
             new Entities.Iletisim
             {
                 Id = new Guid("550de627-2176-43bd-a40a-fce08ebb8465"),
                 KisiId = new Guid("927f71b9-fef6-44ce-b6d8-a8ee86df89e0"),
                 IletisimType = (int)IletisimType.Telefon,
                 Icerik = "05383946677"
             },

              new Entities.Iletisim
              {
                  Id = new Guid("1cdac1b6-f64f-4dd2-91bd-475ff2812de6"),
                  KisiId = new Guid("927f71b9-fef6-44ce-b6d8-a8ee86df89e0"),
                  IletisimType = (int)IletisimType.Telefon,
                  Icerik = "05383948899"
              }
           );
        }
    }
}
