using Microsoft.EntityFrameworkCore;
using Service.Iletisim.Data;
using Service.Iletisim.Enums;
using Service.Iletisim.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Iletisim
{
    public class IletisimRepositoryTests
    {
        private DbContextOptions<IletisimContext> dbContextOptions;
        public IletisimRepositoryTests()
        {
            var dbName = $"IletisimDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<IletisimContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }

        [Fact]
        public async Task GecerliIletisimModelVerildiginde_IletisimOlusturur()
        {
            var repository = await CreateRepositoryAsync();

            var id = new Guid("856b39fb-d802-4574-a0b4-872a12589c60");

            await repository.IletisimKaydet(new Service.Iletisim.Entities.Iletisim
            {
                Id = id,
                IletisimType = (int)IletisimType.Telefon,
                Icerik = "05383945544"
            });

            var iletisim = await repository.GetIletisimById(id);

            Assert.NotNull(iletisim);
            Assert.Equal(id, iletisim.Id);
            Assert.Equal("05383945544", iletisim.Icerik);
            Assert.Equal((int)IletisimType.Telefon, iletisim.IletisimType);
        }

        [Fact]
        public async Task IdIleIletisimGetir_IletisimDoner()
        {
            var repository = await CreateRepositoryAsync();

            var id = new Guid("e85e3cf0-974e-4bc5-9dee-7d47094d039e");

            var iletisim = await repository.GetIletisimById(id);

            Assert.NotNull(iletisim);
            Assert.Equal(id, iletisim.Id);
            Assert.Equal("05383941232", iletisim.Icerik);
            Assert.Equal((int)IletisimType.Telefon, iletisim.IletisimType);
        }

        [Fact]
        public async Task IletisimListesiIstendiginde_IletisimleriDoner()
        {
            var repository = await CreateRepositoryAsync();
            var iletisimList = await repository.GetirIletisimListesi();
            Assert.NotEmpty(iletisimList);
            Assert.Equal(6, iletisimList.Count());
        }

        [Fact]
        public async Task GecerliIletisimIdIle_IletisimSilindiginde_IletisimSiler()
        {
            var repository = await CreateRepositoryAsync();
            var id = new Guid("e85e3cf0-974e-4bc5-9dee-7d47094d039e");
            await repository.IletisimSil(id);

            var iletisimList = await repository.GetirIletisimListesi();
            // Assert
            Assert.NotEmpty(iletisimList);
            Assert.Equal(5, iletisimList.Count());
        }

        [Fact]
        public async Task GecerliKisiIdIle_IletisimKisininIletisimBilgileriIstendiginde_IletisimBilgileriniGetirir()
        {
            var repository = await CreateRepositoryAsync();
            var kisiId = new Guid("856b39fb-d802-4574-a0b4-872a12589c59");
            

            var iletisimList = await repository.GetIletisimBilgileriByKisiId(kisiId);
            // Assert
            Assert.NotEmpty(iletisimList);
            Assert.Equal(3, iletisimList.Count());
        }
        private async Task<IletisimRepository> CreateRepositoryAsync()
        {
            IletisimContext context = new IletisimContext(dbContextOptions);
            await PopulateDataAsync(context);
            return new IletisimRepository(context);
        }
        private async Task PopulateDataAsync(IletisimContext context)
        {

            await context.Iletisim.AddRangeAsync(new List<Service.Iletisim.Entities.Iletisim>
                {
                    new Service.Iletisim.Entities.Iletisim
                    {
                         Id=new Guid("e85e3cf0-974e-4bc5-9dee-7d47094d039e"),
                         KisiId = new Guid("856b39fb-d802-4574-a0b4-872a12589c59"),
                         IletisimType = (int)IletisimType.Telefon,
                         Icerik = "05383941232"
                    },
                    new Service.Iletisim.Entities.Iletisim
                    {
                        Id = new Guid("6eb946fb-e1c4-4c02-a8c6-c4a9423b9fa5"),
                        KisiId = new Guid("856b39fb-d802-4574-a0b4-872a12589c59"),
                        IletisimType = (int)IletisimType.EPosta,
                        Icerik = "abc@abc.com"
                    },
                    new Service.Iletisim.Entities.Iletisim
                    {
                       Id = new Guid("c042ae87-1a13-4cd2-a281-fa7f7b579118"),
                       KisiId = new Guid("856b39fb-d802-4574-a0b4-872a12589c59"),
                       IletisimType = (int)IletisimType.Konum,
                       Icerik = "Ankara"
                    },
                    new Service.Iletisim.Entities.Iletisim
                    {
                          Id = new Guid("1f1dfa36-bbde-4fd3-87bd-4c0aaed711b1"),
                          KisiId = new Guid("927f71b9-fef6-44ce-b6d8-a8ee86df89e0"),
                          IletisimType = (int)IletisimType.Konum,
                          Icerik = "Ankara"
                    },new Service.Iletisim.Entities.Iletisim
                    {
                         Id = new Guid("550de627-2176-43bd-a40a-fce08ebb8465"),
                          KisiId = new Guid("927f71b9-fef6-44ce-b6d8-a8ee86df89e0"),
                          IletisimType = (int)IletisimType.Telefon,
                          Icerik = "05383946677"
                    }
                    ,new Service.Iletisim.Entities.Iletisim
                    {
                        Id = new Guid("1cdac1b6-f64f-4dd2-91bd-475ff2812de6"),
                          KisiId = new Guid("927f71b9-fef6-44ce-b6d8-a8ee86df89e0"),
                          IletisimType = (int)IletisimType.Telefon,
                          Icerik = "05383948899"
                    }
                });

            await context.SaveChangesAsync();
        }
    }
}
