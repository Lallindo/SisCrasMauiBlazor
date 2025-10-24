using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace SisCras.ViewModels;

public partial class MainLayoutViewModel : ObservableObject, IDisposable
{
    private NavigationManager NavigationManager { get; }
    [ObservableProperty]
    private bool _isVisible = false;

    public MainLayoutViewModel(NavigationManager navigationManager)
    {
        NavigationManager = navigationManager;
        NavigationManager.LocationChanged += UpdateNavMenuVisibility;
    }

    public void UpdateNavMenuVisibility(object? sender, LocationChangedEventArgs e)
    {
        var relativeUri = NavigationManager.ToBaseRelativePath(e.Location);

        Debug.WriteLine($"{relativeUri}");

        List<string> routesToHideNav = [
            ""
        ];

        IsVisible = !routesToHideNav.Contains(relativeUri, StringComparer.OrdinalIgnoreCase);
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= UpdateNavMenuVisibility;
    }
}