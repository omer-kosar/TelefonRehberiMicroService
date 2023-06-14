using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Kisi.Data;
using Service.Rapor.Controllers.v1;
using Service.Rapor.Dto;
using Service.Rapor.Enums;
using Service.Rapor.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Rapor
{
    public class ReportsControllerTests
    {
        private DbContextOptions<RaporContext> dbContextOptions;
        public ReportsControllerTests()
        {
            var dbName = $"RaporDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<RaporContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }
        [Fact]
        public async Task GecerliRaporModelIleRaporKaydedildiginde_RaporKaydeder()
        {
            var repository = await CreateRepositoryAsync();
            var reportsController = new ReportsController(repository, null);
            var reportDto = new RaporDto
            {
                Durum = 1,
                TalepEdildigiTarih = DateTimeOffset.UtcNow
            };

            var result = await reportsController.RaporKaydet(reportDto) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(true, Guid.TryParse(result.Value.ToString(), out var iletisimId));
        }

        [Fact]
        public async Task ButunRaporlarGetirilmekIstendiginde_RaporListesiniDoner()
        {
            var repository = await CreateRepositoryAsync();
            var reportsController = new ReportsController(repository, null);

            var actionResult = await reportsController.GetirRaporListesi() as ObjectResult;

            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<RaporListDto>>(actionResult.Value);
            Assert.NotEmpty(actionResult.Value as IEnumerable<RaporListDto>);
        }
        [Fact]
        public async Task RaporIdIleRaporDetayBilgileriListelenmekIstendigindeRaporBilgileriniGetirir()
        {
            var raporBilgiRepository = await CreateRaporBilgiRepositoryAsync();
            var reportsController = new ReportsController(null, raporBilgiRepository);
            var raporId = new Guid("d6245fe2-0947-11ee-b208-54e1ad72c6a1");
            var actionResult = await reportsController.GetirRaporDetayBilgileri(raporId) as ObjectResult;

            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<RaporBilgiListDto>>(actionResult.Value);
            Assert.NotEmpty(actionResult.Value as IEnumerable<RaporBilgiListDto>);
        }

        private async Task<RaporRepository> CreateRepositoryAsync()
        {
            RaporContext context = new RaporContext(dbContextOptions);
            await PopulateDataAsync(context);
            return new RaporRepository(context);
        }
        private async Task<RaporBilgiRepository> CreateRaporBilgiRepositoryAsync()
        {
            RaporContext context = new RaporContext(dbContextOptions);
            await PopulateRaporBilgiDataAsync(context);
            return new RaporBilgiRepository(context);
        }
        private async Task PopulateRaporBilgiDataAsync(RaporContext context)
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
                        TelefonNumarasiSayisi = 0,
                    }
                });

            await context.SaveChangesAsync();
        }
        private async Task PopulateDataAsync(RaporContext context)
        {

            await context.Rapor.AddRangeAsync(new List<Service.Rapor.Entities.Rapor>
                {
                    new Service.Rapor.Entities.Rapor
                    {
                        Id = new Guid("d6245fe2-0947-11ee-b208-54e1ad72c6a1"),
                        Durum = (int)RaporDurum.Tamamlandi,
                        TalepEdildigiTarih = DateTime.Now
                    },
                    new Service.Rapor.Entities.Rapor
                    {
                           Id = new Guid("e50ef18e-0947-11ee-b20b-54e1ad72c6a1"),
                           Durum = (int)RaporDurum.Tamamlandi,
                           TalepEdildigiTarih = DateTime.Now
                    } , new Service.Rapor.Entities.Rapor
                    {
                           Id = new Guid("d2f53771-7a90-4306-bd1c-782e8527ec29"),
                           Durum = (int)RaporDurum.Hazirlaniyor,
                           TalepEdildigiTarih = DateTime.Now
                    }
                });

            await context.SaveChangesAsync();
        }
    }
}
