using System.Collections.Generic;
using System.Net;
using Azure;
using Azure.Storage;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
namespace api;

public class HttpTrigger1
{
    private readonly ILogger _logger;

    public HttpTrigger1(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<HttpTrigger1>();
        CreateAzureCredential();
    }

    [Function("HttpTrigger1")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        
        
        BlobContainerClient blobContainerClient = GetContainerClient("spectrogram-jpegs");
        BlobClient blobClient = blobContainerClient.GetBlobClient("2024-05-01 16:13:59.933634.jpg");

        MemoryStream memoryStream = new MemoryStream();
        var blobContent = blobClient.Download(default, null, false, default);
        blobClient.DownloadTo(memoryStream);
        // var blobResponse = blobContent.GetRawResponse();
        // blobResponse.Body.CopyTo(memoryStream);
        // byte[] imageData = memoryStream.ToArray();
        // var imageBytes = blobResponse.Body;
        
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "image/jpeg");
        response.WriteBytes(memoryStream.ToArray());

        return response;
        
    }
    
    
    private readonly string _azAccountName = Environment.GetEnvironmentVariable("StorageContainerName");
    private readonly string _azAccountKey = Environment.GetEnvironmentVariable("StorageAuthentication"); 
    private readonly string _blobURI = $"https://team16storage.blob.core.windows.net";
    private Azure.Storage.StorageSharedKeyCredential _cred; 
    
    
    public void CreateAzureCredential()
    {
        _cred = new StorageSharedKeyCredential(_azAccountName, _azAccountKey);
    }
    
    public BlobServiceClient BlobClient { get; set; }
    public BlobContainerClient GetContainerClient(string containerName)
    {
        BlobClient = new BlobServiceClient(
            new Uri(_blobURI),
            _cred);
        return BlobClient.GetBlobContainerClient(containerName);
    }
}
