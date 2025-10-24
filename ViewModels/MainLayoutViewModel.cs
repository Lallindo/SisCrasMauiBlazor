using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace SisCras.ViewModels;

public partial class MainLayoutViewModel : ObservableObject, IDisposable
{
    [ObservableProperty]
    private bool _isVisible;

    public MainLayoutViewModel(NavigationManager navigationManager)
    {
        NavigationManager = navigationManager;
        NavigationManager.LocationChanged += UpdateNavMenuVisibility;
    }
    private NavigationManager NavigationManager { get; }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= UpdateNavMenuVisibility;
    }

    public void UpdateNavMenuVisibility(object? sender, LocationChangedEventArgs e)
    {
        var relativeUri = NavigationManager.ToBaseRelativePath(e.Location);

        Debug.WriteLine($"{relativeUri}");

        List<string> routesToHideNav =
        [
            ""
        ];

        IsVisible = !routesToHideNav.Contains(relativeUri, StringComparer.OrdinalIgnoreCase);
    }
}