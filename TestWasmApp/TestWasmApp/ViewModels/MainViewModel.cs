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
        // ImageFromWebsite =
        //     ImageHelper.LoadFromWeb(new Uri("https://cdn.corporatefinanceinstitute.com/assets/private-vs-public.jpeg"));
        // _azureBlobInterface = new AzureBlobInterface();
        // _listener = new EdgeListener();
        // _listener.ImageIsReady += ListenerOnDownloadedImage;
        // _ = Task.Run(() => _listener.Listen(_azureBlobInterface, ImageFromWebsite));
        _azureFunctionInterface = new AzureFunctionInterface();
        ImageFromWebsite = _azureFunctionInterface.GetBlob();
    }

    private void ListenerOnDownloadedImage()
    {
        ImageFromWebsite = _listener.downloadedImage;
    }

    private EdgeListener _listener;
    private AzureFunctionInterface _azureFunctionInterface;
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