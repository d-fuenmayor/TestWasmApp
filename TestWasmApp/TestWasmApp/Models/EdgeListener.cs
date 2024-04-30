using System.Collections.Generic;
using System.Threading;
using Avalonia.Media.Imaging;
using Azure.Messaging.EventHubs.Producer;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Azure;

namespace TestWasmApp.Models;

using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Microsoft.Azure.Devices.Client;
using Azure.Messaging.EventHubs.Processor;

class EdgeListener
{
    private static DeviceClient _deviceClient;
    private static readonly string _eventHubName = "team16-iot-hub";
    private static readonly string _connectionString =
        "Endpoint=sb://iothub-ns-team16-iot-59296121-f83ebc5a70.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=CA/vCBtupvTNiB7VJL6Kf3fO9c8hNJqslAIoTG0zJ2c=;EntityPath=team16-iot-hub";
    private static readonly string _consumerGroupName = "sound-data-consumer";
    private static readonly string deviceName = "pi-edge-device";

    public event Action ImageIsReady;
    public Task<Bitmap?> downloadedImage;

    public EdgeListener()
    {
    }

    public async Task Listen(AzureBlobInterface blobInterface, Task<Bitmap?> bitmap)
    {
        BlobContainerClient containerClient = blobInterface.GetContainerClient("spectrogram-jpegs");

        await using (var consumer = new EventHubConsumerClient(_consumerGroupName, _connectionString, _eventHubName))
        {
            using var cancellationSource = new CancellationTokenSource();

            await foreach (PartitionEvent receivedEvent in consumer.ReadEventsFromPartitionAsync("1", EventPosition.Latest, cancellationSource.Token))
            {
                Console.WriteLine(receivedEvent);
                Console.WriteLine(receivedEvent.Data.Data.ToString());
                BlobClient blobClient = containerClient.GetBlobClient(receivedEvent.Data.Data.ToString());

                Uri image_uri = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.MaxValue);
                Console.WriteLine(image_uri); 
                Thread.Sleep(new TimeSpan(0, 0, 1));
                downloadedImage = ImageHelper.LoadFromWeb(image_uri);
                ImageIsReady();
            }
        }
    }

}