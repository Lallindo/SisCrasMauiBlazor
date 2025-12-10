using FluentValidation;
using SisCras.Domain.Entities;

namespace SisCras.Presentation.Validators;

public class FamiliaUsuarioValidator : AbstractValidator<FamiliaUsuario>
{
    public FamiliaUsuarioValidator()
    {
        RuleFor(x => x.Usuario).SetValidator(new UsuarioValidator()!);
    }
}
