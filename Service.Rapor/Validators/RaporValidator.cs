using FluentValidation;
using Service.Rapor.Dto;

namespace Service.Rapor.Validators
{
    public class RaporValidator : AbstractValidator<RaporDto>
    {
        public RaporValidator()
        {
            RuleFor(x => x.Durum).NotEmpty().WithMessage("Rapor durumu boş olamaz.");
        }
    }
}
