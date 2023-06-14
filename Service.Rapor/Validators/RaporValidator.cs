using FluentValidation;
using Service.Rapor.Dto;

namespace Service.Rapor.Validators
{
    public class RaporValidator : AbstractValidator<RaporDto>
    {
        public RaporValidator()
        {
            RuleFor(x => x.TalepEdildigiTarih).NotNull().WithMessage("Rapor tarihi alanı boş olamaz.");
            RuleFor(x => x).Must(x => x.TalepEdildigiTarih == default(DateTimeOffset)).WithMessage("Rapor tarihi boş olamaz");
            RuleFor(x => x.Durum).NotEmpty().WithMessage("Rapor durumu boş olamaz.");
        }
    }
}
