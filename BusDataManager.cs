using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Dapper;
using Polly;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Data.SqlClient;
using System.Data;
using Newtonsoft;
using Newtonsoft.Json;
using CatchTheBus;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace CatchTheBus
{
    public class BusDataManager
    {
        private readonly string _connectionString = Environment.GetEnvironmentVariable("AzureSQLConnectionString");
        private readonly string _busRealTimeFeedUrl = Environment.GetEnvironmentVariable("RealTimeFeedUrl"); 
        private readonly ILogger _log;

        public BusDataManager(ILogger log)
        {
            _log = log;
        }

        public async Task ProcessBusData()
        {
            var feedTask = DownloadBusData();
            var monitoredRoutesTask = GetMonitoredRoutes();

            Task.WaitAll(new Task[] { feedTask, monitoredRoutesTask });

            var feed = await feedTask;
            var monitoredRoutes = await monitoredRoutesTask;

            // Find all the bus to be monitored
            var buses = feed.Entities.FindAll(e => monitoredRoutes.Contains(e.Vehicle.Trip.RouteId));

            await SaveBusData(buses);
        }

        private async Task<GTFS.RealTime.Feed> DownloadBusData()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_busRealTimeFeedUrl);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var feed = JsonConvert.DeserializeObject<GTFS.RealTime.Feed>(responseString);

            return feed;
        }

        private async Task<List<int>> GetMonitoredRoutes()
        {
            using var conn = new SqlConnection(_connectionString);
            var result = await conn.QueryAsync<int>("web.GetMonitoredRoutes", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        private async Task SaveBusData(List<GTFS.RealTime.Entity> buses)
        {
            // Build payload
            var busData = new JArray();
            buses.ForEach(b =>
            {
                //_log.LogInformation($"{b.Vehicle.VehicleId.Id}: {b.Vehicle.Position.Latitude}, {b.Vehicle.Position.Longitude}");
                var d = new JObject
                {
                    ["DirectionId"] = b.Vehicle.Trip.DirectionId,
                    ["RouteId"] = b.Vehicle.Trip.RouteId,
                    ["VehicleId"] = b.Vehicle.VehicleId.Id,
                    ["Position"] = new JObject
                    {
                        ["Latitude"] = b.Vehicle.Position.Latitude,
                        ["Longitude"] = b.Vehicle.Position.Longitude
                    },
                    ["TimestampUTC"] = Utils.FromPosixTime(b.Vehicle.Timestamp)
                };

                busData.Add(d);
            });
            
            using var conn = new SqlConnection(_connectionString);
            {
                await conn.ExecuteAsync("web.AddBusData", new { payload = busData.ToString() },  commandType: CommandType.StoredProcedure);
            }            
        }
    }
}
