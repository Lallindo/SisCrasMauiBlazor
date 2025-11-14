using CommunityToolkit.Mvvm.ComponentModel;
using SisCras.ApplicationLayer.Services;

namespace SisCras.Presentation.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty] protected bool _isBusy;
    public bool IsNotBusy => !IsBusy; 
}
