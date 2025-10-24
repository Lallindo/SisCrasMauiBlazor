using CommunityToolkit.Mvvm.ComponentModel;

namespace SisCras.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isBusy;

    public bool IsNotBusy => !IsBusy;// Era usado em XAML para não ter que criar um ValueConverter só para inverter o booleano
    // Não é necessário mas pode facilitar na leitura do código FrontEnd
}