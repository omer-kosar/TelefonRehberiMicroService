using Service.Kisi.Dto;
using Service.Kisi.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kisi.UnitTests
{
    public class ValidationTests
    {
        private readonly KisiValidator _kisiValidator;

        public ValidationTests()
        {
            _kisiValidator = new KisiValidator();
        }

        [Theory]
        [InlineData(null, null, null, false)]
        [InlineData("testad", null, null, false)]
        [InlineData(null, "testad", null, false)]
        [InlineData(null, null, "testfirma", false)]
        [InlineData("testad", "testsoyad", null, false)]
        [InlineData("testad", null, "testfirma", false)]
        [InlineData(null, "testad", "testfirma", false)]
        [InlineData("testad", "testad", null, false)]
        [InlineData("testad testad testad testad testad testad testad testad testad testad ", "testad", "test firma", false)]
        [InlineData("testad", "test soyad test soyad test soyad test soyad test soyad test soyad test soyad test soyad test soyad ", "test firma", false)]
        [InlineData("testad", "test soyad", "test firma test firma test firma test firma test firma test firma test firma test firma test firma test firma test firma ", false)]
        [InlineData("testad", "testsoyad", "testfirma", true)]

        public void TestModelValidation(string? ad, string? soyad, string? firma, bool isValid)
        {
            var kisi = new KisiDto
            {
                Ad = ad,
                Soyad = soyad,
                Firma = firma
            };

            Assert.Equal(isValid, ValidateModel(kisi));
        }
        private bool ValidateModel(KisiDto model)
        {
            var result = _kisiValidator.Validate(model);
            return result.IsValid;
        }
    }
}
