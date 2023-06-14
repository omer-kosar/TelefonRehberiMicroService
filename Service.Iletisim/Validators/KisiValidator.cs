using FluentValidation;
using Service.Iletisim.Dto;

namespace Service.Iletisim.Validators
{
    public class IletisimValidator : AbstractValidator<IletisimDto>
    {
        public IletisimValidator()
        {
            RuleFor(x => x.Icerik).NotEmpty().WithMessage("İletişim bilgisi boş olamaz.")
                .MaximumLength(50).WithMessage("İletişim bilgisi 50 karakterden fazla olamaz.");
        }
    }
}
