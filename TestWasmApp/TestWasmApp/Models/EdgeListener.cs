using System.Collections.Generic;
using Microsoft.Extensions.Azure;

namespace TestWasmApp.Models;

using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;

class EdgeListener
{
    private static readonly string connectionString =
        "Endpoint=sb://iothub-ns-team16-iot-59296121-f83ebc5a70.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=CA/vCBtupvTNiB7VJL6Kf3fO9c8hNJqslAIoTG0zJ2c=;EntityPath=team16-iot-hub";

    private static readonly string consumerGroupName = "sound-data-consumer";
    private const string EventHubsCompatibleEndpoint = "TODO: az iot hub show --query properties.eventHubEndpoints.events.endpoint --name {hubname}";  
    private const string EventHubsCompatiblePath = "TODO: {hubname}";  
    private const string IotHubSasKey = "TODO: az iot hub policy show --name service --query primaryKey --hub-name {hubname}";  
    private const string ConsumerGroup = "$Default";


    public void AttemptToConnect()
    {
        try
        {
            var processorHubConsumerClient = new EventHubConsumerClient(consumerGroupName, connectionString);
            Console.WriteLine(processorHubConsumerClient.ConsumerGroup);
            processorHubConsumerClient.ReadEventsAsync(default);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}