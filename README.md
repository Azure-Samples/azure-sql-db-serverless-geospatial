---
page_type: sample
languages:
- tsql
- sql
- csharp
products:
- azure-sql-database
- azure
- dotnet
- dotnet-core
- azure-functions
description: "Real-Time Serverless GeoSpatial Public Transportation GeoFencing Solution"
urlFragment: "azure-sql-db-serverless-geospatial"
---

# Real-Time Serverless GeoSpatial Public Transportation GeoFencing Solution

<!-- 
Guidelines on README format: https://review.docs.microsoft.com/help/onboard/admin/samples/concepts/readme-template?branch=master

Guidance on onboarding samples to docs.microsoft.com/samples: https://review.docs.microsoft.com/help/onboard/admin/samples/process/onboarding?branch=master

Taxonomies for products and languages: https://review.docs.microsoft.com/new-hope/information-architecture/metadata/taxonomies?branch=master
-->

![License](https://img.shields.io/badge/license-MIT-green.svg)

A fully working end-to-end solution, to never miss the bus again. Also, a funny way for learning how to create a serverless solution that leverages real-time public transportation geospatial data. The solution will also show how you can integrate Azure Functions with [IFTTT](https://ifttt.com/home) to get notification on your phone, so you'll be leaving home or office right on time.

## How it works

The sample uses an Azure Function to monitor Real-Time Public Transportation Data, available as [GTS-Realtime Feed](https://gtfs.org/reference/realtime/v2/) and published by several public transportation companies like, for example, the [King County Metro](https://kingcounty.gov/depts/transportation/metro/travel-options/bus/app-center/developer-resources.aspx).

Every 15 seconds the Azure Function will wake up and get the GTS Realtime Feed. It will send data to Azure SQL where, thanks to [Geospatial](https://docs.microsoft.com/en-us/sql/relational-databases/spatial/spatial-data-sql-server) support, data will be stored and processed to see if any of the monitored buses (in table `dbo.MonitoredRoutes`) is a Geofence (stored in `dbo.GeoFences` table). If yes, the function will call an IFTTT endpoint to send a notification to a mobile phone. Better leave the office to be sure not to miss the bus!


## Pre-Requisites

To run this sample, you need to have [Azure Function Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=windows%2Ccsharp%2Cbash) or [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio 2019](https://visualstudio.microsoft.com/vs/) and an Azure SQL database to use. If you need help to create an Azure SQL database, take a look here: [Running the samples](https://github.com/yorek/azure-sql-db-samples#running-the-samples).

## Settings
Before running the sample locally or on Azure, make sure to create a `local.settings.json` file using the provided template. There are three settings that you must specify:

- `RealtimeFeedUrl` should point to a valid [GTFS-Realtime](https://gtfs.org/reference/realtime/v2/#message-feedheader) url. The sample uses the one provided by [King County Metro](https://kingcounty.gov/depts/transportation/metro/travel-options/bus/app-center/developer-resources.aspx). 

- `IFTTTUrl` should point to the url created by a IFTTT webhook. It something like `https://maker.ifttt.com/trigger/{event}/with/key/{key}`. You can get the correct url by accessing the Documentation section of the [Webhook page of IFTTT](https://ifttt.com/maker_webhooks).

- `AzureSQLConnectionString` is the connection string to the Azure SQL you want to use for running the sample

## Run sample locally

You can run and debug the sample locally, as local execution of Azure Function is fully supported by any of the aforementioned tools.

## Deploy on Azure

The script `./azure-deploy.sh` will take care of everything. Make sure you set the correct values for you subscription for:

```
resourceGroup=
appName=
storageName=
location=
```

The script needs has been tested on Linux Ubuntu and the [Windows Subsystem for Linux](https://docs.microsoft.com/en-us/windows/wsl/) or the [Cloud Shell](https://shell.azure.com/).

The following resources will be created for you:

- Azure Functions with Consumption plan
- Azure Application Insights
- Azure Storage Account

The Azure SQL DB *will not* be created by the script.