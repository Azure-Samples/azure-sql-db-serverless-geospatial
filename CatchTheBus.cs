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
        public static IActionResult ShowBusData([HttpTrigger("get", Route = "bus")] HttpRequest req, ILogger log)
        {
            var busData = new {
                geometry = "POLYGON((-122.14357282700348 47.616901066671886,-122.141025341366 47.61685232450776,-122.14101421569923 47.617249758593886,-122.14283305463597 47.61725350816795,-122.14283861681452 47.61845704045888,-122.14351164303936 47.6184795362212,-122.14357282700348 47.616901066671886))"
            };

            return new OkObjectResult(busData);
        }

    }
}
