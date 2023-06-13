using FluentValidation;
using Service.Kisi.Dto;

namespace Service.Kisi.Validators
{
    public class KisiValidator : AbstractValidator<KisiDto>
    {
        public KisiValidator()
        {
            RuleFor(x => x.Ad).NotEmpty().WithMessage("Ad alanı boş olamaz.")
                .MaximumLength(50).WithMessage("Ad alanı 50 karakterden fazla olamaz.");
            RuleFor(x => x.Soyad).NotEmpty().WithMessage("Soyad alanı boş olamaz.")
                .MaximumLength(50).WithMessage("Soyad alanı 50 karakterden fazla olamaz.");
            RuleFor(x => x.Firma).NotEmpty().WithMessage("Firma alanı boş olamaz.")
                .MaximumLength(50).WithMessage("Firma alanı 50 karakterden fazla olamaz.");
        }
    }
}
