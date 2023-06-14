using Service.Iletisim.Dto;
using Service.Iletisim.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Iletisim
{
    public class ValidationTests
    {
        private readonly IletisimValidator _iletisimValidator;

        public ValidationTests()
        {
            _iletisimValidator = new IletisimValidator();
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("testadtestadtestadtestadtestadtestadtestadtestadtestadtestadtestad", false)]
        [InlineData("testad", true)]

        public void TestModelValidation(string? icerik, bool isValid)
        {
            var iletisim = new IletisimDto
            {
                Icerik = icerik
            };

            Assert.Equal(isValid, ValidateModel(iletisim));
        }
        private bool ValidateModel(IletisimDto model)
        {
            var result = _iletisimValidator.Validate(model);
            return result.IsValid;
        }
    }
}
