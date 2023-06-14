using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Iletisim.Controllers.v1;
using Service.Iletisim.Data;
using Service.Iletisim.Dto;
using Service.Iletisim.Enums;
using Service.Iletisim.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Iletisim
{
    public class ContactsControllerTests
    {


        private DbContextOptions<IletisimContext> dbContextOptions;
        public ContactsControllerTests()
        {
            var dbName = $"IletisimDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<IletisimContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }

        [Fact]
        public async Task GecerliIletisimModelIleIletisimKaydedildiginde_IletisimKaydeder()
        {
            var repository = await CreateRepositoryAsync();
            var contactsController = new ContactsController(repository);
            var iletisimDto = new IletisimDto
            {
                Id = Guid.NewGuid(),
                KisiId = new Guid("856b39fb-d802-4574-a0b4-872a12589c59"),
                IletisimType = (int)IletisimType.Telefon,
                Icerik = "05383943344"
            };
            var result = await contactsController.KaydetIletisim(iletisimDto) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(true, Guid.TryParse(result.Value.ToString(), out var iletisimId));
        }

        [Fact]
        public async Task KayitliIletisimIdIleSilinmekIstendiginde_IletisimSiler()
        {
            var repository = await CreateRepositoryAsync();
            var contactsController = new ContactsController(repository);

            var id = Guid.Parse("e85e3cf0-974e-4bc5-9dee-7d47094d039e");
            var result = await contactsController.IletisimSilById(id) as StatusCodeResult;
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }
        [Fact]
        public async Task KayitliOlmayanIdIleIletisimSilinmekIstendiginde_BulunamadiDoner()
        {
            var repository = await CreateRepositoryAsync();
            var contactsController = new ContactsController(repository);

            var id = Guid.Parse("f4f4e3bf-afa6-4399-87b5-a3fe17572c4d");

            var result = await contactsController.IletisimSilById(id) as ObjectResult;
            Assert.NotNull(result);

            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal($"Contact was not able to found with id:{id}", result.Value);
        }

        [Fact]
        public async Task KonumIleKonumRaporuGetirilmekIstendiginde_KonumRaporuDoner()
        {
            var repository = await CreateRepositoryAsync();
            var contactsController = new ContactsController(repository);

            var konum = "Ankara";

            var result = await contactsController.GetirKonumRaporu(konum) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<KonumRaporuDto>(result.Value);
            Assert.NotNull(result.Value as KonumRaporuDto);
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
