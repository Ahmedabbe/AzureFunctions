using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace AzureFunctions
{
    public static class SaveDynamicDataToTableStorage
    {
        private static HttpClient client = new HttpClient();
        [return: Table("DynamicMessages")]

        [FunctionName("SaveDynamicDataToTableStorage")]
        public static dynamic Run([IoTHubTrigger("messages/events", Connection = "IotHubConnection")]EventData message, ILogger log)
        {
            log.LogInformation($"Incomming dynamic message: {Encoding.UTF8.GetString(message.Body.Array)}");

            try
            {
                var payload = JsonConvert.DeserializeObject<dynamic>(Encoding.UTF8.GetString(message.Body.Array));

                payload.PartitionKey = payload.Type;
                payload.RowKey = Guid.NewGuid().ToString();
                log.LogInformation("Saving dynamic data to Table Storage");

                return payload;
            }
            catch
            {
                log.LogInformation("Failed to deserialize message. No dynamic data saved");
            }

            return null;
        }
    }
}