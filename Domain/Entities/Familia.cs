using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using SisCras.Domain.Enums;

namespace SisCras.Domain.Entities;

public class Familia
{
    public ConfiguracaoFamiliarEnum ConfiguracaoFamiliar { get; set; }
    public ICollection<FamiliaUsuario> FamiliaUsuarios { get; set; } = [];
    public int Id { get; set; } = 0;
    public ICollection<Prontuario> Prontuarios { get; set; } = [];

    public Usuario? Responsavel
    {
        get
        {
            if (FamiliaUsuarios == null || FamiliaUsuarios.Count == 0) return null;
            return (from fu 
                    in FamiliaUsuarios 
                    where fu.Parentesco == ParentescoEnum.Responsavel && fu.DataSaida == null 
                    select fu.Usuario)
                .FirstOrDefault();
        }
    }

    public float RendaTotal
    {
        get
        {
            if (FamiliaUsuarios == null || FamiliaUsuarios.Count == 0) return 0;
            return FamiliaUsuarios.Sum(fu => fu.Usuario.RendaBruta);
        }
    }

    public float RendaPerCapita
    {
        get
        {
            if (RendaTotal == 0) return 0;
            return RendaTotal / FamiliaUsuarios.Count;
        }
    }

    public ObservableCollection<Usuario> Usuarios
    {
        get
        {
            if (FamiliaUsuarios == null || FamiliaUsuarios.Count == 0) return [];
            return new ObservableCollection<Usuario>(from fu in FamiliaUsuarios select fu.Usuario);
        }
    }

    public Task AddUsuario(Usuario usuario, ParentescoEnum parentesco)
    {
        try
        {
            FamiliaUsuario newUsuario = new() { Usuario = usuario, Parentesco = parentesco };
            FamiliaUsuarios.Add(newUsuario);
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            return Task.FromException(ex);
        }
    }

    public Task ToggleAtivoUsuario(FamiliaUsuario usuario)
    {
        if (usuario.DataSaida != null
            && DateTime.Now - usuario.DataSaida.Value.ToDateTime(new TimeOnly()) < TimeSpan.FromDays(30))
            usuario.DataSaida = null;
        else
            usuario.DataSaida = DateOnly.FromDateTime(DateTime.Now);
        return Task.CompletedTask;
    }
}
