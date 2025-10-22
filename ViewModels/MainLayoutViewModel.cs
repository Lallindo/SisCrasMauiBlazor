using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace SisCras.ViewModels;

public partial class MainLayoutViewModel : ObservableObject, IDisposable
{
    NavigationManager _NavigationManager { get; }
    [ObservableProperty]
    bool _IsVisible = false;

    public MainLayoutViewModel(NavigationManager navigationManager)
    {
        _NavigationManager = navigationManager;
        _NavigationManager.LocationChanged += UpdateNavMenuVisibility;
    }

    public void UpdateNavMenuVisibility(object? sender, LocationChangedEventArgs e)
    {
        var relativeUri = _NavigationManager.ToBaseRelativePath(e.Location);

        Debug.WriteLine($"{relativeUri}");

        List<string> routesToHideNav = [
            ""
        ];

        IsVisible = !routesToHideNav.Contains(relativeUri, StringComparer.OrdinalIgnoreCase);
    }

    public void Dispose()
    {
        _NavigationManager.LocationChanged -= UpdateNavMenuVisibility;
    }
}