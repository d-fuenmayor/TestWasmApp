namespace TestWasmApp.ViewModels;

public class MainViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia! This is a placeholder app made by Daniel Alfonso Fuenmayor that proves he can do CI/CD for an Avalonia WASM Browser App!";
#pragma warning restore CA1822 // Mark members as static
}