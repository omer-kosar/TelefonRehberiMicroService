﻿using Microsoft.EntityFrameworkCore;
using Service.Kisi.Data;
using Service.Rapor.Enums;
using Service.Rapor.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Rapor
{
    public class RaporRepositoryTests
    {
        private DbContextOptions<RaporContext> dbContextOptions;
        public RaporRepositoryTests()
        {
            var dbName = $"RaporDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<RaporContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }


        [Fact]
        public async Task IdIleRaporGetir_RaporDoner()
        {
            var repository = await CreateRepositoryAsync();

            var id = new Guid("d6245fe2-0947-11ee-b208-54e1ad72c6a1");

            var rapor = await repository.GetirRaporById(id);

            Assert.NotNull(rapor);
            Assert.Equal(id, rapor.Id);
        }

        [Fact]
        public async Task GecerliRaporModelVerildiginde_RaporOlusturur()
        {
            var repository = await CreateRepositoryAsync();

            var id = new Guid("856b39fb-d802-4574-a0b4-872a12589c60");
            // Act
            await repository.RaporKaydet(new Service.Rapor.Entities.Rapor
            {
                Id = id,
                Durum = (int)RaporDurum.Tamamlandi,
                TalepEdildigiTarih = DateTime.Now
            });

            // Assert
            var kisi = await repository.GetirRaporById(id);

            Assert.NotNull(kisi);
            Assert.Equal(id, kisi.Id);
        }
        [Fact]
        public async Task RaporListesiIstendiginde_RaporlariDoner()
        {
            var repository = await CreateRepositoryAsync();
            var raporListList = await repository.GetirRaporListesi();
            Assert.NotEmpty(raporListList);
            Assert.Equal(2, raporListList.Count());
        }
        [Fact]
        public async Task RaporGuncellenmekIstendiginde_RaporGunceller()
        {
            var repository = await CreateRepositoryAsync();
            var raporId = new Guid("d2f53771-7a90-4306-bd1c-782e8527ec29");
            var rapor = await repository.GetirRaporById(raporId);
            rapor.Durum = 2;
            repository.RaporGuncelle(rapor);
            var guncelRapor = await repository.GetirRaporById(raporId);

            Assert.NotNull(guncelRapor);
            Assert.Equal(rapor.Durum, guncelRapor.Durum);
            Assert.Equal((int)RaporDurum.Tamamlandi, guncelRapor.Durum);
        }
        private async Task<RaporRepository> CreateRepositoryAsync()
        {
            RaporContext context = new RaporContext(dbContextOptions);
            await PopulateDataAsync(context);
            return new RaporRepository(context);
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
