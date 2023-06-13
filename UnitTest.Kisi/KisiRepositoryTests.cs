using Microsoft.EntityFrameworkCore;
using Service.Kisi.Data;
using Service.Kisi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Kisi
{
    public class KisiRepositoryTests
    {
        private DbContextOptions<KisiContext> dbContextOptions;
        public KisiRepositoryTests()
        {
            var dbName = $"KisiDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<KisiContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }

        [Fact]
        public async Task IdIleKisiGetir_KisiDoner()
        {
            var repository = await CreateRepositoryAsync();

            var id = new Guid("856b39fb-d802-4574-a0b4-872a12589c59");
            // Act
            var kisi = await repository.GetirKisiById(id);

            // Assert
            Assert.NotNull(kisi);
            Assert.Equal(id, kisi.Id);
            Assert.Equal("John", kisi.Ad);
        }
        [Fact]
        public async Task GecerliKisiModelVerildiginde_KisiOlusturur()
        {
            var repository = await CreateRepositoryAsync();

            var id = new Guid("856b39fb-d802-4574-a0b4-872a12589c60");
            // Act
            await repository.KisiKaydet(new Service.Kisi.Entities.Kisi
            {
                Id = id,
                Ad = "Bill",
                Soyad = "Gates",
                Firma = "Microsoft Coorporation"
            });

            // Assert
            var kisi = await repository.GetirKisiById(id);

            Assert.NotNull(kisi);
            Assert.Equal(id, kisi.Id);
            Assert.Equal("Bill", kisi.Ad);
        }
        private async Task<KisiRepository> CreateRepositoryAsync()
        {
            KisiContext context = new KisiContext(dbContextOptions);
            await PopulateDataAsync(context);
            return new KisiRepository(context);
        }
        [Fact]
        public async Task KisiListesiIstendiginde_KisileriDoner()
        {
            var repository = await CreateRepositoryAsync();
            var kisiList = await repository.GetirKisiListesi();
            Assert.NotEmpty(kisiList);
            Assert.Equal(3, kisiList.Count());
        }

        private async Task PopulateDataAsync(KisiContext context)
        {

            await context.Kisi.AddRangeAsync(new List<Service.Kisi.Entities.Kisi>
                {
                    new Service.Kisi.Entities.Kisi
                    {
                        Id = new Guid("856b39fb-d802-4574-a0b4-872a12589c59"),
                        Ad = "John",
                        Soyad = "Doe",
                        Firma = "ABC Corporation"
                    },
                    new Service.Kisi.Entities.Kisi
                    {
                        Id = new Guid("927f71b9-fef6-44ce-b6d8-a8ee86df89e0"),
                        Ad = "Jane",
                        Soyad = "Smith",
                        Firma = " XYZ Ltd."
                    },
                    new Service.Kisi.Entities.Kisi
                    {   Id = new Guid("a5ce0ec5-1c1a-4a15-86f9-cdde439a92a2"),
                        Ad = "Michael",
                        Soyad = "Johnson",
                        Firma = "QWERTY Holdings"
                    }
                });

            await context.SaveChangesAsync();
        }
    }
}
