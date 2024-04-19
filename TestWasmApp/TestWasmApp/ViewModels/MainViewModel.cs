namespace TestWasmApp.ViewModels;

public class MainViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "This is a placeholder web app that proves CI/CD for an Avalonia WASM Browser App is working!";
#pragma warning restore CA1822 // Mark members as static
}