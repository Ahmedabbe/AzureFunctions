using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunctions.Models
{
    public class DhtMessage : TableEntity
    {
        public string Type { get; set; }
        public long EpochTime { get; set; }
        public string DeviceId { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }
}
