using Microsoft.EntityFrameworkCore;
using Service.Kisi.Data;
using Service.Rapor.Enums;
using Service.Rapor.Repositories;
using Service.Rapor.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Rapor
{
    public class RaporBilgiRepositoryTests
    {
        private DbContextOptions<RaporContext> dbContextOptions;
        public RaporBilgiRepositoryTests()
        {
            var dbName = $"RaporDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<RaporContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }

        [Fact]
        public async Task IdIleRaporBilgiGetir_RaporBilgiDoner()
        {
            var repository = await CreateRepositoryAsync();

            var id = new Guid("07f4b594-0948-11ee-b20c-54e1ad72c6a1");

            var rapor = await repository.GetirRaporBilgiById(id);

            Assert.NotNull(rapor);
            Assert.Equal(id, rapor.Id);
        }

        [Fact]
        public async Task GecerliRaporBilgiModelVerildiginde_RaporBilgiOlusturur()
        {
            var repository = await CreateRepositoryAsync();

            var id = new Guid("856b39fb-d802-4574-a0b4-872a12589c60");
            // Act
            await repository.RaporBilgiKaydet(new Service.Rapor.Entities.RaporBilgi
            {
                Id = id,
                RaporId = new Guid("d6245fe2-0947-11ee-b208-54e1ad72c6a1"),
                KonumBilgisi = "Mersin",
                KisiSayisi = 0,
                TelefonNumarasiSayisi = 0,
            });

            // Assert
            var raporBilgi = await repository.GetirRaporBilgiById(id);

            Assert.NotNull(raporBilgi);
            Assert.Equal(id, raporBilgi.Id);
        }

        [Fact]
        public async Task RaporIdIleOlusturulanRaporBilgileriIstendiginde_RaporDetayBilgileriniDoner()
        {
            var repository = await CreateRepositoryAsync();
            var raporId = new Guid("d6245fe2-0947-11ee-b208-54e1ad72c6a1");
            var raporDetayBilgileri = await repository.GetirRaporDetayBilgiList(raporId);
            Assert.NotEmpty(raporDetayBilgileri);
            Assert.Equal(1, raporDetayBilgileri.Count());
        }
        private async Task<RaporBilgiRepository> CreateRepositoryAsync()
        {
            RaporContext context = new RaporContext(dbContextOptions);
            await PopulateDataAsync(context);
            return new RaporBilgiRepository(context);
        }
        private async Task PopulateDataAsync(RaporContext context)
        {

            await context.RaporBilgi.AddRangeAsync(new List<Service.Rapor.Entities.RaporBilgi>
                {
                    new Service.Rapor.Entities.RaporBilgi
                    {
                          Id = new Guid("07f4b594-0948-11ee-b20c-54e1ad72c6a1"),
                          RaporId = new Guid("d6245fe2-0947-11ee-b208-54e1ad72c6a1"),
                          KisiSayisi = 2,
                          TelefonNumarasiSayisi = 3,
                          KonumBilgisi = "Ankara"
                    },
                    new Service.Rapor.Entities.RaporBilgi
                    {
                         Id = new Guid("7ea050d6-0948-11ee-b20d-54e1ad72c6a1"),
                         RaporId = new Guid("e50ef18e-0947-11ee-b20b-54e1ad72c6a1"),
                         KonumBilgisi = "Mersin",
                         KisiSayisi = 0,
                         TelefonNumarasiSayisi = 0
                    }
                });

            await context.SaveChangesAsync();
        }
    }
}
