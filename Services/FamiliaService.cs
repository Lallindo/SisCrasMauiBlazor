using SisCras.Models;
using SisCras.Repositories;

namespace SisCras.Services;

public class FamiliaService(IFamiliaRepository familiaRepository) : BaseService<Familia>(familiaRepository), IFamiliaService
{
    IFamiliaRepository FamiliaRepository { get; } = familiaRepository;
}