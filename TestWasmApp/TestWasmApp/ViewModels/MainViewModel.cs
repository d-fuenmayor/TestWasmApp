using System;
using System.Threading.Tasks;

namespace TestWasmApp.ViewModels;

using TestWasmApp.Models;
using Avalonia.Media.Imaging;
public class MainViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public MainViewModel()
    {
        
    }
    EdgeListener _listener = new EdgeListener();
    // public Bitmap? ImageFromBinding { get; } = ImageHelper.LoadFromResource(new Uri("avares://LoadingImages/Assets/abstract.jpg"));
    public Task<Bitmap?> ImageFromWebsite { get; } = ImageHelper.LoadFromWeb(new Uri("https://plus.unsplash.com/premium_photo-1708589335486-1645a4730e13?q=80&w=2670&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"));

    public void StartListening()
    {
        _listener.AttemptToConnect();
    }
    
    
#pragma warning restore CA1822 // Mark members as static
}