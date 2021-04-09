using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AzureFunctions.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctions
{
    public static class GetDataFromTableStorage
    {
        [FunctionName("GetDataFromTableStorage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [Table("Messages")] CloudTable cloudtable,
            ILogger log)
        {
            string limit = req.Query["limit"];
            string orderby = req.Query["orderby"];

            IEnumerable<DhtMessage> results = await cloudtable.ExecuteQuerySegmentedAsync(new TableQuery<DhtMessage>(), null);
            results = results.OrderByDescending(ts => ts.Timestamp);

            if (orderby == "desc")
                results = results.OrderByDescending(ts => ts.Timestamp);
            if (limit != null)
                results = results.Take(int.Parse(limit));
           

            return new OkObjectResult(results);
        }
    }
}

