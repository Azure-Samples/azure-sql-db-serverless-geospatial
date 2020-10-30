using System;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Dapper;
using Polly;

namespace CatchTheBus
{    
    public static class CatchTheBus
    {
        [FunctionName("CatchTheBus")]
        public static void Run([TimerTrigger("*/15 * * * * *")]TimerInfo myTimer, ILogger log)
        {
            
        }
    }
}
