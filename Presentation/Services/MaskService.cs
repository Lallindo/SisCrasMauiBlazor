using SisCras.Presentation.Enums;

namespace SisCras.Presentation.Services;

public class MaskService : IMaskService
{
    public string GetDigits(string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        return new string(input.Where(char.IsDigit).ToArray());
    }
    
    public string ApplyMaskCpf(string cpf)
    {
        string digits = GetDigits(cpf);
        if (digits.Length > 11) digits = digits.Substring(0, 10);
        
        return digits.Length switch
        {
            >= 10 => $"{digits.Substring(0, 3)}.{digits.Substring(3, 3)}.{digits.Substring(6, 3)}-{digits.Substring(9)}",
            >= 7 => $"{digits.Substring(0, 3)}.{digits.Substring(3, 3)}.{digits.Substring(6)}",
            >= 4 => $"{digits.Substring(0, 3)}.{digits.Substring(3)}",
            _ => digits,
        };
    }

    public string ApplyMaskRg(string rg)
    {
        string digits = GetDigits(rg);
        if (digits.Length > 9) digits = digits.Substring(0, 9);
        
        return digits.Length switch
        {
            >= 9 => $"{digits.Substring(0, 2)}.{digits.Substring(2, 3)}.{digits.Substring(5, 3)}-{digits.Substring(8)}",
            >= 6 => $"{digits.Substring(0, 2)}.{digits.Substring(2, 3)}.{digits.Substring(5)}",
            >= 3 => $"{digits.Substring(0, 2)}.{digits.Substring(2)}",
            _ => digits,
        };
    }

    public string ApplyMaskDinheiro(string dinheiro)
    {
        string digits = GetDigits(dinheiro);
        if (string.IsNullOrEmpty(digits)) return "0,00";

        if (decimal.TryParse(digits, out decimal decimalValue))
        {
            decimalValue = decimalValue / 100M;
            return string.Format(new System.Globalization.CultureInfo("pt-BR"), "{0:N2}", decimalValue);
        }

        return digits;
    }

    public string ApplyMaskNis(string nis)
    {
        string digits = GetDigits(nis);
        if (digits.Length > 11) digits = digits.Substring(0, 11);

        return digits.Length switch
        {
            >= 11 => $"{digits.Substring(0, 3)}.{digits.Substring(3, 5)}.{digits.Substring(8, 2)}-{digits.Substring(10)}",
            >= 9 => $"{digits.Substring(0, 3)}.{digits.Substring(3, 5)}.{digits.Substring(8)}",
            >= 4 => $"{digits.Substring(0, 3)}.{digits.Substring(3)}",
            _ => digits,
                
        };
    }
}