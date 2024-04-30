using System;
using System.Threading.Tasks;
using ReactiveUI;

namespace TestWasmApp.ViewModels;

using TestWasmApp.Models;
using Avalonia.Media.Imaging;
public class MainViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public MainViewModel()
    {
        _azureBlobInterface = new AzureBlobInterface();
        _listener = new EdgeListener();
        _listener.ImageIsReady += ListenerOnDownloadedImage;
        _ = Task.Run(() => _listener.Listen(_azureBlobInterface, ImageFromWebsite));
    }

    private void ListenerOnDownloadedImage()
    {
        ImageFromWebsite = _listener.downloadedImage;
    }

    private EdgeListener _listener;

    private AzureBlobInterface _azureBlobInterface;
    // public Bitmap? ImageFromBinding { get; } = ImageHelper.LoadFromResource(new Uri("avares://LoadingImages/Assets/abstract.jpg"));
    private Task<Bitmap?> _imageFromWebsite;

    public Task<Bitmap?> ImageFromWebsite
    {
        get => _imageFromWebsite;
        set => this.RaiseAndSetIfChanged(ref _imageFromWebsite, value);
    } 

    
    
#pragma warning restore CA1822 // Mark members as static
}