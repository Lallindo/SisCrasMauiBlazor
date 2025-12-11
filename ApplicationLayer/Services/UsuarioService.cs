using SisCras.Domain.Entities;
using SisCras.Infrastructure.Repositories;

namespace SisCras.ApplicationLayer.Services;

public class UsuarioService(IUsuarioRepository usuarioRepository)
    : BaseService<Usuario>(usuarioRepository), IUsuarioService
{
    private IUsuarioRepository UsuarioRepository { get; } = usuarioRepository;

    public async Task<List<Familia?>> GetFamiliasFromUsuario(int id)
    {
        return await UsuarioRepository.GetFamiliasFromUsuario(id);
    }

    public async Task<List<Familia?>> GetFamiliasFromUsuario(Usuario usuario)
    {
        return await UsuarioRepository.GetFamiliasFromUsuario(usuario);
    }

    public async Task<Familia?> GetActiveFamiliaFromUsuario(int id)
    {
        return await UsuarioRepository.GetActiveFamiliaFromUsuario(id);
    }

    public async Task<Familia?> GetActiveFamiliaFromUsuario(Usuario usuario)
    {
        return await UsuarioRepository.GetActiveFamiliaFromUsuario(usuario);
    }

    public async Task<Usuario?> GetByCpf(string cpfParaBuscar)
    {
        return await UsuarioRepository.GetByCpf(cpfParaBuscar);
    }

    public async Task<Usuario?> GetByCpf(Usuario usuario)
    {
        return await UsuarioRepository.GetByCpf(usuario);
    }

    public async Task<Usuario?> GetByNome(string nome)
    {
        return await UsuarioRepository.GetByNome(nome);
    }

    public async Task<Usuario?> GetByNome(Usuario usuario)
    {
        return await UsuarioRepository.GetByNome(usuario);
    }

    public async Task<Usuario?> GetByDataNascimento(DateOnly dataNascimento)
    {
        return await UsuarioRepository.GetByDataNascimento(dataNascimento);
    }

    public async Task<Usuario?> GetByDataNascimento(Usuario usuario)
    {
        return await UsuarioRepository.GetByDataNascimento(usuario);
    }

    public async Task<List<Prontuario?>> GetAllProntuariosByUsuarioSearch(string? nome, string? cpf, string? nis)
    {
        return await UsuarioRepository.GetAllProntuariosByUsuarioSearch(nome, cpf, nis);
    }

    public async Task<List<Prontuario?>> GetAllProntuariosByUsuarioSearch(Usuario usuario)
    {
        return await UsuarioRepository.GetAllProntuariosByUsuarioSearch(usuario);
    }

    public async Task<Usuario?> GetUsuarioByUsuarioSearch(string? nome, string? cpf, string? nis)
    {
        return await UsuarioRepository.GetUsuarioByUsuarioSearch(nome, cpf, nis);
    }

    public async Task<Usuario?> GetUsuarioByUsuarioSearch(Usuario usuario)
    {
        return await UsuarioRepository.GetUsuarioByUsuarioSearch(usuario);
    }

    public async Task<FamiliaUsuario?> DeactivateActiveFamiliaUsuario(int id)
    {
        return await UsuarioRepository.DeactivateActiveFamiliaUsuario(id);
    }

    public async Task<FamiliaUsuario?> DeactivateActiveFamiliaUsuario(Usuario usuario)
    {
        return await UsuarioRepository.DeactivateActiveFamiliaUsuario(usuario);
    }

    public async Task<FamiliaUsuario> ReactivateFamiliaUsuario(int id)
    {
        return await UsuarioRepository.ReactivateFamiliaUsuario(id);
    }

    public async Task<FamiliaUsuario> ReactivateFamiliaUsuario(FamiliaUsuario familiaUsuario)
    {
        return await UsuarioRepository.ReactivateFamiliaUsuario(familiaUsuario.Id);
    }
    
    public static bool CheckCpf(string? cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return false;

        string cleanCpf = cpf.Replace(".", "").Replace("-", "").Trim();
    
        if (cleanCpf.Length != 11) return false;

        if (cleanCpf.Distinct().Count() == 1) return false;

        List<int> valores = new();
    
        foreach (char digito in cleanCpf)
        {
            if (!char.IsDigit(digito)) return false;
            valores.Add((int)char.GetNumericValue(digito));
        }

        if (GetDigitCpf(valores.GetRange(0, 9)) != valores[9]) return false;
    
        if (GetDigitCpf(valores.GetRange(0, 10)) != valores[10]) return false;
    
        return true;
    }

    public static bool CheckCpf(Usuario usuario)
    {
        return CheckCpf(usuario.Cpf);
    }

    private static int GetDigitCpf(List<int> list)
    {
        int mult = list.Count + 1; 
        int sum = 0;
    
        foreach (int numero in list)
        {
            sum += numero * mult;
            mult--;
        }

        int resto = sum % 11;
        int digit = 11 - resto;

        return (digit >= 10) ? 0 : digit;
    }
    
    public async Task DeleteIfEmptyAsync(Usuario usuario, CancellationToken cancellationToken = default)
    {
        // A exclusão só deve ocorrer se o usuário já tiver sido persistido (Id > 0)
        // e seus dados de identificação estiverem vazios.
        if (usuario.Id > 0 && usuario.IsEmpty())
        {
            await UsuarioRepository.DeleteAsync(usuario, cancellationToken);
        }
    }
}
