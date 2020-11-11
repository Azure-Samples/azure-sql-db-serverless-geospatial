/*
	Full documentation on "Accessing data in a CSV file referencing an Azure blob storage location" here:
	https://docs.microsoft.com/en-us/sql/relational-databases/import-export/examples-of-bulk-access-to-data-in-azure-blob-storage?view=sql-server-ver15
*/
create master key encryption by password = 'Your_VERY_StrongPazzw0rd!'
go

create database scoped credential TransitCredentials
with identity = 'SHARED ACCESS SIGNATURE',
secret = 'XXX'
go

create external data source Transit
with (
	type = blob_storage,
	location = 'https://XXXX.blob.core.windows.net/transit',
	credential = TransitCredentials
)

delete from dbo.[Routes];
insert into dbo.[Routes]
	([Id], [AgencyId], [ShortName], [Description], [Type])
select 
	[Id], [AgencyId], [ShortName], [Description], [Type]
from 
openrowset
	( 
		bulk 'routes.txt', 
		data_source = 'Transit', 
		formatfile = 'routes.fmt', 
		formatfile_data_source = 'Transit', 
		firstrow=2,
		format='csv'
	) t;
go

select * from dbo.[Routes] where [Description] like '%Education Hill%'
go
