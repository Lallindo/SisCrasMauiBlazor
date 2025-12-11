using FluentValidation;
using SisCras.Domain.Entities;
using SisCras.Domain.Enums;

namespace SisCras.Presentation.Validators;

public class FamiliaUsuarioValidator : AbstractValidator<FamiliaUsuario>
{
    public FamiliaUsuarioValidator()
    {
        // Use When para aplicar as regras de validação
        // apenas quando a propriedade 'Ativo' for verdadeira.
        When(fu => fu.Ativo, () =>
        {
            RuleFor(x => x.Usuario).SetValidator(new UsuarioValidator()!);

            RuleFor(fu => fu.Parentesco)
                .NotEqual(ParentescoEnum.Default).WithMessage("Selecione um parentesco.");
        });
    }
}