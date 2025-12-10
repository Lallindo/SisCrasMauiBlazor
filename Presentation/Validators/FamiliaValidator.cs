using FluentValidation;
using SisCras.Domain.Entities;

namespace SisCras.Presentation.Validators;

public class FamiliaValidator : AbstractValidator<Familia>
{
    public FamiliaValidator()
    {
        RuleForEach(x => x.FamiliaUsuarios).SetValidator(new FamiliaUsuarioValidator());
    }
}
