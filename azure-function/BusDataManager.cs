using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Dapper;
using Polly;
using System.Threading.Tasks;
using System.Net.Http;

namespace CatchTheBus
{
    public class BusDataManager
    {
        private readonly string _busRealTimeFeedUrl = "https://s3.amazonaws.com/kcm-alerts-realtime-prod/vehiclepositions_enhanced.json";
        private readonly ILogger _log;

        public BusDataManager(ILogger log)
        {
            _log = log;
        }

        public async Task DownloadBusData()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_busRealTimeFeedUrl);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            

            // Get busses we are interested in
            // TODO

            // Filter only the busses we want to monitor
            // TODO

        }
    }
}
