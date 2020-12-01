# Logging service application
.NET Core 3.1 and Entity Framework Core

## In Visual Studio 2019:

1. Make sure to change User ID and Password in database connection string with your credentials located in appsettings.json file. Currently they are set to:

	- User ID = root
	- Password = myPassword

2. Open the Package Manager Console (Tools->Nuget Package Manager->Package Manager Console). Make sure you have LoggingService.Infrastructure project selected.

3. Run the following commands:

	- Add-Migration Initial -o Persistance/Migrations
	- Update-Database
	
Now press F5 and run the application.

## In case docker-compose up doesn't work:

1. Locate appsettings.json file in Published code folder. Make sure to change User ID and Password in database connection string with your credentials located in appsettings.json file.
2. Open IIS and create new web site. Set web site like this:

	- Site name: LoggingServiceAPI
	- Physical path: navigate to Published code folder
	- Type: http/https (if you have SSL certificate)
	- IP address: leave All Unassigned
	- Port: 5555
	- Optional:
		- Hostname: SSL certificate will enforce host name, for example if SSL certificate is ".apx.com" then hostname should be "loggingservice.apx.com"
		- SSL certificate: select certificate from list

	Clicking "Ok" button should start web site immediatley

3. In IIS navigate to Application Pools and find web site that you have just created. Right click on web site and go to "Basic Settings" and change .NET CLR version to "No Managed Code".
