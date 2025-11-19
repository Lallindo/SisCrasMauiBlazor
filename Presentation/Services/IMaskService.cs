using SisCras.Presentation.Enums;

namespace SisCras.Presentation.Services;

public interface IMaskService
{
    string GetDigits(string input);
    string ApplyMaskCpf(string cpf);
    string ApplyMaskRg(string rg);
    string ApplyMaskDinheiro(string dinheiro);
    string ApplyMaskNis(string nis);
}