using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace CatchTheBus
{    
    public static class CatchTheBus
    {
        [FunctionName("GetBusData")]
        public async static Task GetBusData([TimerTrigger("*/15 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            var m = new BusDataManager(log);
            await m.ProcessBusData();
        }

        [FunctionName("ShowBusData")]
        public static async Task<IActionResult> ShowBusData([HttpTrigger("get", Route = "bus")] HttpRequest req, ILogger log)
        {        
            var m = new BusDataManager(log);            
            return new OkObjectResult(await m.GetMonitoredBusData());
        }

    }
}
