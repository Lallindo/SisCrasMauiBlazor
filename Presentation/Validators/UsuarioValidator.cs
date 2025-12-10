using FluentValidation;
using SisCras.Domain.Entities;

namespace SisCras.Presentation.Validators;

public class UsuarioValidator : AbstractValidator<Usuario>
{
    public UsuarioValidator()
    {
        RuleFor(u => u.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caractéres");
        
        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Matches(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$").WithMessage("CPF inválido.");
    }
}
