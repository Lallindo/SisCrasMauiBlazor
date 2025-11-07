using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using SisCras.Domain.Entities.Enums;

namespace SisCras.Domain.Entities;

public partial class Familia : ObservableObject
{
    [ObservableProperty] private ConfiguracaoFamiliarEnum _configuracaoFamiliar;
    [ObservableProperty] private ICollection<FamiliaUsuario> _familiaUsuarios = [];
    [ObservableProperty] private int _id;
    [ObservableProperty] private ICollection<Prontuario> _prontuarios = [];

    public Usuario? Responsavel
    {
        get
        {
            if (_familiaUsuarios == null || _familiaUsuarios.Count == 0) return null;
            return (from fu in _familiaUsuarios where fu.Parentesco == ParentescoEnum.Responsavel select fu.Usuario).FirstOrDefault();
        }
    }

    public float RendaTotal
    {
        get
        {
            if (_familiaUsuarios == null || _familiaUsuarios.Count == 0) return 0;
            return _familiaUsuarios.Sum(fu => fu.Usuario.RendaBruta);
        }
    }

    public float RendaPerCapita
    {
        get
        {
            if (RendaTotal == 0) return 0;
            return RendaTotal / _familiaUsuarios.Count;
        }
    }

    public ObservableCollection<Usuario> Usuarios
    {
        get
        {
            if (_familiaUsuarios == null || _familiaUsuarios.Count == 0) return [];
            return new(from fu in _familiaUsuarios select fu.Usuario);
        }
    }

    public Task AddUsuario(Usuario usuario, ParentescoEnum parentesco)
    {
        try
        {
            FamiliaUsuario newUsuario = new() { Usuario = usuario, Parentesco = parentesco };
            _familiaUsuarios.Add(newUsuario);
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
            && DateTime.Now - usuario.DataSaida.Value.ToDateTime(new TimeOnly()) < TimeSpan.FromDays(30) )
        {
            usuario.DataSaida = null;
        }
        else
        {
            usuario.DataSaida = DateOnly.FromDateTime(DateTime.Now);
        }
        return Task.CompletedTask;
    }
}