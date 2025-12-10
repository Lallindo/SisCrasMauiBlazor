using FluentValidation;
using FluentValidation.Validators;
using SisCras.Domain.Entities;
using SisCras.ApplicationLayer.Services;
using SisCras.Domain.Enums;

namespace SisCras.Presentation.Validators;

public class UsuarioValidator : AbstractValidator<Usuario>
{
    public UsuarioValidator()
    {
        RuleFor(u => u.Nome)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caractéres");
        
        RuleFor(u => u.Cpf)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .MaximumLength(14).WithMessage("O CPF deve ter no máximo 14 dígitos.")
            .Must(UsuarioService.CheckCpf).WithMessage("CPF inválido.");

        RuleFor(u => u.Rg)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O RG é obrigatório.")
            .MaximumLength(14).WithMessage("O RG deve ter no máximo 14 dígitos.");

        RuleFor(u => u.Sexo)
            .Cascade(CascadeMode.Stop)
            .NotEqual(SexoEnum.Default).WithMessage("Selecione o sexo do usuário.");
        
        RuleFor(u => u.Raca)
            .Cascade(CascadeMode.Stop)
            .NotEqual(RacaEnum.Default).WithMessage("Selecione a raça do usuário.");
        
        RuleFor(u => u.Profissao)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("A profissão é obrigatória.");

        RuleFor(u => u.Nis)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O NIS é obrigatório.");

        RuleFor(u => u.DataNascimento)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(new DateOnly(1900, 1, 1)).WithMessage("Data de nascimento inválida.")
            .NotEqual(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Data atual não é válida.");

        RuleFor(u => u.EstadoCivil)
            .Cascade(CascadeMode.Stop)
            .NotEqual(EstadoCivilEnum.Default).WithMessage("Selecione o estado civil do usuário.");

        RuleFor(u => u.Escolaridade)
            .Cascade(CascadeMode.Stop)
            .NotEqual(EscolaridadeEnum.Default).WithMessage("Selecione a escolaridade do usuário.");

        RuleFor(u => u.OrientacaoSexual)
            .Cascade(CascadeMode.Stop)
            .NotEqual(OrientacaoSexualEnum.Default).WithMessage("Selecione a orientação sexual do usuário.");

        RuleFor(u => u.RendaBruta)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("A renda bruta é obrigatória.");
    }
}
