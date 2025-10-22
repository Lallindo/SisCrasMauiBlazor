using SisCras.Models;
using SisCras.Repositories;

namespace SisCras.Services;

public class ProntuarioService(IProntuarioRepository prontuarioRepository) : BaseService<Prontuario>(prontuarioRepository), IProntuarioService
{
    IProntuarioRepository _ProntuarioRepository { get; } = prontuarioRepository;
}