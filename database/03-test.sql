--POINT(-122.14217921194788 47.61704789214207) --in

declare @payload nvarchar(max) = N'[{
	"DirectionId": 1,
	"RouteId": 1,
	"VehicleId": 1,
	"Position": {
		"Latitude": 45.617005097778616,
		"Longitude": -121.14203287537352
	},
	"TimestampUTC": "20201030"
},{
	"DirectionId": 1,
	"RouteId": 1,
	"VehicleId": 2,
	"Position": {
		"Latitude": 47.617005097778616,
		"Longitude": -122.14203287537352
	},
	"TimestampUTC": "20201031"
}]';

exec web.AddBusData @payload;
go

--POINT(-122.14305525703526 47.62001356715828) --out
declare @payload nvarchar(max) = N'[{
	"DirectionId": 1,
	"RouteId": 1,
	"VehicleId": 1,
	"Position": {
		"Latitude": 47.617005097778616,
		"Longitude": -122.14203287537352
	},
	"TimestampUTC": "20201030"
},{
	"DirectionId": 1,
	"RouteId": 1,
	"VehicleId": 2,
	"Position": {
		"Latitude": 47.62001356715828,
		"Longitude": -122.14305525703526
	},
	"TimestampUTC": "20201031"
}]';

exec web.AddBusData @payload;
go