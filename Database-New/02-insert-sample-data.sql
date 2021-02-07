delete from dbo.[MonitoredRoutes]
delete from  dbo.[GeoFences] 
go

select * from dbo.[Routes] where [Description] like '%Education Hill%'
go
insert into dbo.[MonitoredRoutes] values (100113)
go

insert into dbo.[GeoFences] 
	([Name], [GeoFence])
values 
	('Overlake Stop', 'POLYGON((-122.14359028995352 47.61824519124585,-122.14360975757847 47.616519550427654,-122.13966755206604 47.61652611188751,-122.13968701903617 47.617280676597375,-122.142821316476 47.61730036079834,-122.142821316476 47.618186139853435,-122.14359028995352 47.61824519124585))')
go

select * from dbo.[MonitoredRoutes] 
select * from dbo.[GeoFences] 
go