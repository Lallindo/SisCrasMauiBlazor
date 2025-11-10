using SisCras.Models;
using SisCras.Repositories;

namespace SisCras.Services;

public class UsuarioService(IUsuarioRepository usuarioRepository) : BaseService<Usuario>(usuarioRepository), IUsuarioService
{
    IUsuarioRepository UsuarioRepository { get; } = usuarioRepository;
}