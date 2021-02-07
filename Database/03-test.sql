-- Cleanup sample data
delete from dbo.[BusData] where [VehicleId] in (1,2)
delete from dbo.[GeoFenceLog] where [VehicleId] in (1,2)
delete from dbo.[GeoFencesActive] where [VehicleId] in (1,2)

-- Two bus, both entering the GeoFence
declare @payload nvarchar(max) = N'[{
	"DirectionId": 1,
	"RouteId": 100001,
	"VehicleId": 1,
	"Position": {
		"Latitude": 47.61705102765316,
		"Longitude": -122.14291865504012 
	},
	"TimestampUTC": "20201030"
},{
	"DirectionId": 1,
	"RouteId": 100001,
	"VehicleId": 2,
	"Position": {
		"Latitude": 47.61705102765316,
		"Longitude": -122.14291865504012 
	},
	"TimestampUTC": "20201031"
}]';

exec web.AddBusData @payload;
go

-- See sample data
select * from dbo.[BusData] where [VehicleId] in (1,2)
select * from dbo.[GeoFencesActive] where [VehicleId] in (1,2)
select * from dbo.[GeoFenceLog] where [VehicleId] in (1,2) order by Id desc
go

-- Two bus, one leaving the GeoFence
declare @payload nvarchar(max) = N'[{
	"DirectionId": 1,
	"RouteId": 100001,
	"VehicleId": 1,
	"Position": {
		"Latitude": 47.61705102765316,
		"Longitude": -122.14291865504012 
	},
	"TimestampUTC": "20201030"
},{
	"DirectionId": 1,
	"RouteId": 100001,
	"VehicleId": 2,
	"Position": {
		"Latitude": 46.61705102765316,
		"Longitude": -122.14291865504012 
	},
	"TimestampUTC": "20201031"
}]';

exec web.AddBusData @payload;
go

-- See sample data
select * from dbo.[BusData] where [VehicleId] in (1,2)
select * from dbo.[GeoFencesActive] where [VehicleId] in (1,2)
select * from dbo.[GeoFenceLog] where [VehicleId] in (1,2) order by Id desc
go
