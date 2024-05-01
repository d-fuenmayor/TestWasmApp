using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;

namespace TestWasmApp.Models;

public class AzureFunctionInterface
{
    private static readonly HttpClient client = new()
    {
        BaseAddress = new Uri("http://localhost:7071")
    };
    
    public AzureFunctionInterface()
    {
        
    }

    public async Task<Bitmap> GetBlob()
    {
        using HttpResponseMessage response = await client.GetAsync("/api/HttpTrigger1");
        
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadAsByteArrayAsync();
        return new Bitmap(new MemoryStream(data));
    }
}