using Service.Rapor.Dto;
using Service.Rapor.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Rapor
{
    public class ValidationTests
    {
        private readonly RaporValidator _raporValidator;
        public ValidationTests()
        {
            _raporValidator = new RaporValidator();
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        public void TestModelValidation(int durum, bool isValid)
        {
            var rapor = new RaporDto
            {
                Durum = durum,
                TalepEdildigiTarih = DateTime.Now
            };

            Assert.Equal(isValid, ValidateModel(rapor));
        }
        private bool ValidateModel(RaporDto model)
        {
            var result = _raporValidator.Validate(model);
            return result.IsValid;
        }
    }
}
