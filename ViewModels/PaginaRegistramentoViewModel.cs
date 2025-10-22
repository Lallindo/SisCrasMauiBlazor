using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SisCras.Models;
using SisCras.Services;

namespace SisCras.ViewModels;

public partial class PaginaRegistramentoViewModel(
    IProntuarioService prontuarioService,
    IFamiliaService familiaService,
    ILoggedUserService loggedUserService
    ) : ObservableObject
{
    private readonly IProntuarioService _prontuarioService = prontuarioService;
    private readonly IFamiliaService _familiaService = familiaService;
    private readonly ILoggedUserService _loggedUserService = loggedUserService;

    [ObservableProperty]
    Prontuario _Prontuario = new();
    [ObservableProperty]
    ObservableCollection<Usuario> _Usuarios = [new()];

    [RelayCommand]
    private async Task CreateNewUsuario()
    {
        Usuarios.Add(new());
    }

    /*[RelayCommand]
    private async Task CreateProntuarioAsync()
    {
        try
        {
            // 1. Obter o técnico logado e suas informações (incluindo o CRAS)
            var tecnicoLogado = _loggedUserService.GetCurrentUser();
            if (tecnicoLogado?.CrasInfo is null)
            {
                Debug.WriteLine("Erro: Técnico não está logado ou não possui CRAS associado.");
                return;
            }

            // 2. Criar a família e seus membros (sem IDs, o EF vai gerar)
            // Em um cenário real, estes dados viriam da UI.
            Familia novaFamilia = new()
            {
                FamiliaUsuarios =
                [
                    new() { Usuario = new() { Nome = "Bruno" }, Parentesco = "Responsável", Ativo = true },
                    new() { Usuario = new() { Nome = "Emelly" }, Parentesco = "Cônjuge", Ativo = true }
                ]
            };

            // 3. Salvar a família e seus usuários no banco. O EF se encarrega de criar todos.
            await _familiaService.AddAsync(novaFamilia);

            // 4. Montar o objeto Prontuario com as referências corretas
            Prontuario.FamiliaId = novaFamilia.Id; // FK para a família recém-criada
            Prontuario.TecnicoId = tecnicoLogado.Id; // FK para o técnico logado
            Prontuario.CrasId = tecnicoLogado.CrasInfo.Id; // FK para o CRAS do técnico
            Prontuario.FormaDeAcesso = "Demanda Espontânea"; // Exemplo
            Prontuario.Codigo = 12345; // Exemplo
            Prontuario.DataCriacao = DateOnly.FromDateTime(DateTime.Today);

            await _prontuarioService.AddAsync(Prontuario);

            Debug.WriteLine($"Prontuário {Prontuario.Id} criado com sucesso para a família {novaFamilia.Id}!");

            await Shell.Current.GoToAsync("PaginaListagem");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erro ao criar prontuário: {ex.Message}");
        }
    }*/
}