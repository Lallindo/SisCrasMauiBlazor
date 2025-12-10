using FluentValidation;
using SisCras.Domain.Entities;

namespace SisCras.Presentation.Validators;

public class ProntuarioValidator : AbstractValidator<Prontuario>
{
    public ProntuarioValidator()
    {
        RuleFor(x => x.Familia).SetValidator(new FamiliaValidator());
    }
}
