using FluentValidation;
using SisCras.Presentation.DTOs;

namespace SisCras.Presentation.Validators;

public class TecnicoValidator : AbstractValidator<TecnicoDto>
{
    public TecnicoValidator()
    {
        RuleFor(t => t.Nome)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caractéres.");
        
        RuleFor(t => t.Login)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Login é obrigatório.")
            .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caractéres.");
        
        RuleFor(t => t.Senha)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Senha é obrigatória.")
            .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");

        RuleFor(t => t.ConfSenha)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Confirme a senha.")
            .Equal(t => t.Senha).WithMessage("As senhas devem ser iguais.");
    }
}