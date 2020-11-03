/*
    Return the last 50 points of a specific Vehicle
*/
with cte as
(
	select top (50)
		*,
		cast([TimestampUTC] as datetimeoffset) at time zone 'Pacific Standard Time' as TimestampLocal
	from
		dbo.[BusData]
	where
		[VehicleId] = 7373
	order by
		Id desc
)
select 
	geography::UnionAggregate([Location]).ToString() 
from 
	cte
go

/*
    Find the last 50 points within the GeoFence
*/
declare @gf as geography = geography::STGeomFromText(
	'POLYGON((-122.14357282700348 47.616901066671886,-122.141025341366 47.61685232450776,-122.14101421569923 47.617249758593886,-122.14283305463597 47.61725350816795,-122.14283861681452 47.61845704045888,-122.14351164303936 47.6184795362212,-122.14357282700348 47.616901066671886))',
	4326
);
select top (50)
	*, [Location].ToString(), geography::STGeomCollFromText('GEOMETRYCOLLECTION(' + [Location].ToString() + ', ' + @gf.ToString() + ')', 4326).ToString()
from
	dbo.[BusData]
where
	[RouteId] = 100113 
and 
	[DirectionId] = 1
and
	[Location].STWithin(@gf) = 1
order by
	Id desc
go

/*
    Draw the last 500 points of a specic Vehicle
*/
declare @gf as geography = geography::STGeomFromText(
	'POLYGON((-122.14357282700348 47.616901066671886,-122.141025341366 47.61685232450776,-122.14101421569923 47.617249758593886,-122.14283305463597 47.61725350816795,-122.14283861681452 47.61845704045888,-122.14351164303936 47.6184795362212,-122.14357282700348 47.616901066671886))',
	4326
);
with cte as
(
	select top (500)
		*
	from
		dbo.[BusData]
	where
		[VehicleId] = 7383
	order by
		Id desc
)
select 
	geography::STGeomCollFromText('GEOMETRYCOLLECTION(' + geography::UnionAggregate([Location]).ToString() + ', ' + @gf.ToString() + ')', 4326).ToString() 
from 
	cte
go