using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace SisCras.Presentation.ViewModels;

public partial class MainLayoutViewModel : ObservableObject, IDisposable
{
    [ObservableProperty] private bool _isNavVisible;
    [ObservableProperty] private bool _isBackButtonVisible;

    public MainLayoutViewModel(NavigationManager navigationManager, IJSRuntime jsRuntime)
    {
        NavigationManager = navigationManager;
        _jsRuntime = jsRuntime;
        NavigationManager.LocationChanged += UpdateNavMenuVisibility;
        NavigationManager.LocationChanged += UpdateBackButtonVisibility;
        
    }

    private NavigationManager NavigationManager { get; }
    private IJSRuntime? _jsRuntime;

    public void Dispose()
    {
        NavigationManager.LocationChanged -= UpdateNavMenuVisibility;
        NavigationManager.LocationChanged -= UpdateBackButtonVisibility;
    }

    public void UpdateNavMenuVisibility(object? sender, LocationChangedEventArgs e)
    {
        var relativeUri = NavigationManager.ToBaseRelativePath(e.Location);

        Debug.WriteLine($"{relativeUri}");

        List<string> routesToHideNav =
        [
            ""
        ];

        IsNavVisible = !routesToHideNav.Contains(relativeUri, StringComparer.OrdinalIgnoreCase);
    }
    
    public void UpdateBackButtonVisibility(object? sender, LocationChangedEventArgs e)
    {
        var relativeUri = NavigationManager.ToBaseRelativePath(e.Location);

        Debug.WriteLine($"{relativeUri}");

        List<string> routesToHideNav =
        [
            "",
            "familias"
        ];

        IsBackButtonVisible = !routesToHideNav.Contains(relativeUri.ToString(), StringComparer.OrdinalIgnoreCase);
    }

    public async Task GoBack()
    {
        await _jsRuntime.InvokeVoidAsync("history.back");
    }
}