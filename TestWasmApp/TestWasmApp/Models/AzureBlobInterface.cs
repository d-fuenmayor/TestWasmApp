using System;
using System.Runtime;

namespace TestWasmApp.Models;

using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

public class AzureBlobInterface
{
    public AzureBlobInterface()
    {
        CreateAzureCredential();
    }
    
    private readonly string _azAccountName = Environment.GetEnvironmentVariable("StorageContainerName");
    private readonly string _azAccountKey = Environment.GetEnvironmentVariable("StorageAuthentication"); 
    private readonly string _blobURI = $"https://team16storage.blob.core.windows.net";
    private Azure.Storage.StorageSharedKeyCredential _cred; 
    
    public BlobServiceClient BlobClient { get; set; }
    
    
    
    
    public void CreateAzureCredential()
    {
        _cred = new StorageSharedKeyCredential(_azAccountName, _azAccountKey);
    }
    
    public BlobContainerClient GetContainerClient(string containerName)
    {
        BlobClient = new BlobServiceClient(
            new Uri(_blobURI),
            _cred);
        return BlobClient.GetBlobContainerClient(containerName);
    }
}