using SisCras.Models;
using SisCras.Models.Enums;
using System;

namespace SisCras.Tests
{
    public static class ModelFactory
    {
        public static Cras CreateCras(string nome = "CRAS Teste")
        {
            return new Cras { Nome = nome };
        }

        public static Usuario CreateUsuario(
            string nome = "Usuário Teste",
            string cpf = "12345678901",
            string nis = "12345678901")
        {
            return new Usuario 
            { 
                Nome = nome,
                NomeSocial = null,
                Cpf = cpf,
                Rg = "1234567",
                Nis = nis,
                DataNascimento = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                Sexo = SexoEnum.Masculino,
                EstadoCivil = EstadoCivilEnum.Solteiro,
                OrientacaoSexual = OrientacaoSexualEnum.Heterossexual,
                Raca = RacaEnum.Branca,
                Escolaridade = EscolaridadeEnum.MedCompleto,
                FonteRenda = FonteRendaEnum.TrabalhoFormal,
                Ocupacao = "Desenvolvedor",
                Profissao = "TI",
                RendaBruta = 2000.0f
            };
        }

        public static Familia CreateFamilia(ConfiguracaoFamiliarEnum configuracao = ConfiguracaoFamiliarEnum.FamiliaNuclear)
        {
            return new Familia 
            { 
                ConfiguracaoFamiliar = configuracao
            };
        }

        public static Tecnico CreateTecnico(
            string nome = "Técnico Teste",
            string login = "tecnico",
            string senha = "senha123")
        {
            return new Tecnico 
            { 
                Nome = nome,
                Login = login,
                Senha = senha
            };
        }

        public static Prontuario CreateProntuario(
            int crasId,
            int familiaId,
            int tecnicoId,
            int codigo = 1001,
            string formaAcesso = "Espontânea")
        {
            return new Prontuario 
            { 
                Codigo = codigo,
                DataCriacao = DateOnly.FromDateTime(DateTime.Now),
                FormaDeAcesso = formaAcesso,
                CrasId = crasId,
                FamiliaId = familiaId,
                TecnicoId = tecnicoId
            };
        }

        public static FamiliaUsuario CreateFamiliaUsuario(
            int familiaId,
            int usuarioId,
            ParentescoEnum parentesco = ParentescoEnum.Filho,
            bool ativo = true)
        {
            return new FamiliaUsuario 
            { 
                FamiliaId = familiaId,
                UsuarioId = usuarioId,
                Parentesco = parentesco,
                Ativo = ativo
            };
        }

        public static TecnicoCras CreateTecnicoCras(
            int tecnicoId,
            int crasId,
            DateOnly? dataEntrada = null,
            DateOnly? dataSaida = null)
        {
            return new TecnicoCras 
            { 
                TecnicoId = tecnicoId,
                CrasId = crasId,
                DataEntrada = dataEntrada ?? DateOnly.FromDateTime(DateTime.Now),
                DataSaida = dataSaida
            };
        }
        
        public static Usuario CreateUsuarioWithCustomProperties(
            string nome,
            string cpf,
            string nis,
            SexoEnum sexo = SexoEnum.Masculino,
            EstadoCivilEnum estadoCivil = EstadoCivilEnum.Solteiro,
            EscolaridadeEnum escolaridade = EscolaridadeEnum.MedCompleto,
            float rendaBruta = 2000.0f)
        {
            return new Usuario 
            { 
                Nome = nome,
                NomeSocial = null,
                Cpf = cpf,
                Rg = "1234567",
                Nis = nis,
                DataNascimento = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                Sexo = sexo,
                EstadoCivil = estadoCivil,
                OrientacaoSexual = OrientacaoSexualEnum.Heterossexual,
                Raca = RacaEnum.Branca,
                Escolaridade = escolaridade,
                FonteRenda = FonteRendaEnum.TrabalhoFormal,
                Ocupacao = "Desenvolvedor",
                Profissao = "TI",
                RendaBruta = rendaBruta
            };
        }
        public static Prontuario CreateProntuarioWithFamiliaAndUsuarios(
            int crasId,
            int familiaId,
            int tecnicoId,
            List<Usuario> usuarios,
            int codigo = 1001,
            string formaAcesso = "Espontânea")
        {
            var prontuario = CreateProntuario(crasId, familiaId, tecnicoId, codigo, formaAcesso);
    
            // Create FamiliaUsuario relationships for each user
            var familiaUsuarios = usuarios.Select((usuario, index) => 
                    CreateFamiliaUsuario(familiaId, usuario.Id, 
                        index == 0 ? ParentescoEnum.Responsavel : ParentescoEnum.Filho, 
                        true))
                .ToList();

            return prontuario;
        }
    }
}