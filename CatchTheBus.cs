using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Dapper;
using Polly;
using System.Threading.Tasks;

namespace CatchTheBus
{    
    public static class CatchTheBus
    {
        [FunctionName("CatchTheBus")]
        public async static Task Run([TimerTrigger("*/15 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            var m = new BusDataManager(log);
            await m.ProcessBusData();
        }
    }
}
