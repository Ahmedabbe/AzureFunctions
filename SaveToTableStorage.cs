using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctions.Models;
using System;

namespace AzureFunctions
{
    public static class SaveToTableStorage
    {
        private static HttpClient client = new HttpClient();

        [FunctionName("SaveToTableStorage")]
        [return: Table("Messages")]
        public static DhtMessage Run([IoTHubTrigger("messages/events", Connection = "IotHubConnection")]EventData message, ILogger log)
        {
            log.LogInformation($"Incomming message: {Encoding.UTF8.GetString(message.Body.Array)}");

            try
            {
                var payload = JsonConvert.DeserializeObject<DhtMessage>(Encoding.UTF8.GetString(message.Body.Array));

                payload.PartitionKey = "dht";
                payload.RowKey = Guid.NewGuid().ToString();
                log.LogInformation("Saving data to Table Storage");

                return payload;
            }
            catch
            {
                log.LogInformation("Failed to deserialize message. No data saved");
            }

            return null;
            
        }
    }
}