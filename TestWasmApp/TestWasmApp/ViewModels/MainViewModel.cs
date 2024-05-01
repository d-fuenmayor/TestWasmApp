using System;
using System.Threading.Tasks;
using ReactiveUI;

namespace TestWasmApp.ViewModels;

using TestWasmApp.Models;
using Avalonia.Media.Imaging;
public class MainViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static

    // public async Task LoadImage()
    // {
    //     Task<Bitmap> imageTask = _azureFunctionInterface.GetBlob();
    //     ImageFromWebsite = await imageTask;
    // }
    

    // private void ListenerOnDownloadedImage()
    // {
    //     ImageFromWebsite = _listener.downloadedImage;
    // }

    private EdgeListener _listener;
    private AzureFunctionInterface _azureFunctionInterface;
    private AzureBlobInterface _azureBlobInterface;
    // public Bitmap? ImageFromBinding { get; } = ImageHelper.LoadFromResource(new Uri("avares://LoadingImages/Assets/abstract.jpg"));
    Bitmap _imageFromWebsite;
    
    public Bitmap ImageFromWebsite
    {
        get => _imageFromWebsite;
        set => this.RaiseAndSetIfChanged(ref _imageFromWebsite, value);
    }
    // public Task<Bitmap?> ImageFromWebsite { get; } = ImageHelper.LoadFromWeb(new Uri(
    //                 "https://cdn.corporatefinanceinstitute.com/assets/private-vs-public.jpeg"));

    public async void StartImageProcess()
    {
        try
        {
            Console.WriteLine("Trying to get image.");
            _azureFunctionInterface = new AzureFunctionInterface();
            // ImageFromWebsite =
            //     ImageHelper.LoadFromWeb(new Uri("https://www.nickrains.com/wp-content/uploads/2014/07/MainPic.jpg"));
            Task<Bitmap?> imageTask = _azureFunctionInterface.GetBlobAsync();
            ImageFromWebsite = await imageTask;
            Console.WriteLine("Completed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    
    
#pragma warning restore CA1822 // Mark members as static
}