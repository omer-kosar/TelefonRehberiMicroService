using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Kisi.Controllers.v1;
using Service.Kisi.Data;
using Service.Kisi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Service.Kisi.Dto;

namespace UnitTest.Kisi
{
    public class PersonsControllerTest
    {
        private DbContextOptions<KisiContext> dbContextOptions;
        public PersonsControllerTest()
        {
            var dbName = $"KisiDb_{DateTime.Now.ToFileTimeUtc()}";
            dbContextOptions = new DbContextOptionsBuilder<KisiContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
        }
        [Fact]
        public async Task KisiIdIleKisiIstendiginde_KisiGetiri()
        {
            var repository = await CreateRepositoryAsync();
            var personsController = new PersonsController(repository);

            var id = Guid.Parse("856b39fb-d802-4574-a0b4-872a12589c59");
            var result = await personsController.GetirKisiById(id) as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsAssignableFrom<KisiDto>(result.Value);
            Assert.NotNull(result.Value as KisiDto);
        }
        [Fact]
        public async Task KayitliOlmayanKisiIdIleGetirildiginde_BulunamadiDoner()
        {
            var repository = await CreateRepositoryAsync();
            var personsController = new PersonsController(repository);
            var id = Guid.Parse("f4f4e3bf-afa6-4399-87b5-a3fe17572c4d");
            var result = await personsController.GetirKisiById(id) as StatusCodeResult;
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }
        [Fact]
        public async Task ButunKisilerGetirilmekIstendiginde_KisiListesiniDoner()
        {
            var repository = await CreateRepositoryAsync();
            var personsController = new PersonsController(repository);
            var actionResult = await personsController.GetirKisiListesi() as ObjectResult;
            // Act
            // Assert
            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
            Assert.IsAssignableFrom<IEnumerable<KisiListDto>>(actionResult.Value);
            Assert.NotEmpty(actionResult.Value as IEnumerable<KisiListDto>);
        }

        [Fact]
        public async Task GecerliKisiModelIleKisiKaydedildiginde_KisiKaydeder()
        {
            var repository = await CreateRepositoryAsync();
            var personsController = new PersonsController(repository);
            var kisiDto = new KisiDto
            {
                Id = Guid.NewGuid(),
                Ad = "Steve",
                Soyad = "Kaufman",
                Firma = "Linq Ltd. Şt."
            };
            var result = await personsController.KisiKaydet(kisiDto) as StatusCodeResult;
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async Task KayitliKisiIdIleSilinmekIstendiginde_KisiSiler()
        {
            var repository = await CreateRepositoryAsync();
            var personsController = new PersonsController(repository);

            var id = Guid.Parse("856b39fb-d802-4574-a0b4-872a12589c59");
            var result = await personsController.KisiSil(id) as StatusCodeResult;
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }
        [Fact]
        public async Task KayitliOlmayanIdIleKisiSilinmekIstendiginde_BulunamadiDoner()
        {
            var repository = await CreateRepositoryAsync();
            var personsController = new PersonsController(repository);

            var id = Guid.Parse("f4f4e3bf-afa6-4399-87b5-a3fe17572c4d");

            var result = await personsController.KisiSil(id) as ObjectResult;
            Assert.NotNull(result);

            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal($"Person was not able to found with id:{id}", result.Value);
        }
        private async Task<KisiRepository> CreateRepositoryAsync()
        {
            KisiContext context = new KisiContext(dbContextOptions);
            await PopulateDataAsync(context);
            return new KisiRepository(context);
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
