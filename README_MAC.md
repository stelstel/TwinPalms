# Steps to run TwinPalms backend API on Mac

As Mac does not support running the SSMS (Sql server management studio) directly, we have to run it, inside a docker container and connect to it from our codebase. 

Follow below steps to setup in mac.

1. Run SQL Server in Docker container

2. Run Azure Datastudio and connect to the SQL Server (that runs in the docker container)

3. Inside Visual Studio, provide the connection string inside appsettings.json , as below

	** "sqlConnection": "server=localhost,1433; Database=TwinPalmsReportManagement; User=sa; 	 Password=<Your password>; Integrated Security=false" **

4. Run Migrations in Visual studio and update database (Make sure the SQL Server is up and running (the above 2 steps), before proceeding)
		a. Install NuGet Package manager console in Visual studio through extensions
		b. Run Add-Migrations InitialCreate inside the Nuget Package manager console
		c. run Update-Database 
		
5. Start backend API application, by giving **dotnet build** and **dotnet run** commands in terminal. (*Note*: You should be in the TwinPalmsKPI folder)

6. Once the application is run, access the APIs,
	for eg: http://localhost/swagger/index.html. for swaggar and http://localhost/api/FbReports for seeing fbreports.
	
**NOTE:**  From the front end TwinPalms Client application, make sure the URLs are properly given as per the swagger url formats, in the /DataReports/utils/misc.js file, to fetch and show the backend api data properly.

# Steps to run SQL Server inside docker container**

**Install Docker Desktop** 
https://docs.docker.com/docker-for-mac/install/

**SQL Server:**
Open terminal and run SQL Server in docker

Check if the sqlserver is already up and running or not.

**Docker ps -a**

If not running, enter this  command 👇

docker  run -d --name  sqlserver -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=your password' -p 1433:1433 mcr.microsoft.com/mssql/server:2019-latest

If it is already there than just run it, by giving

**docker start <container name> ** in terminal
	eg: docker start sqlserver
	
after that open Azure data studio and connect to SQL Server.

To connect to your DB in Azure data studio you have to use the below connectino properties:
host: localhost
userId: sa
password: Provide the one, that you have given in the above docker command for the SA_PASSWORD. 
eg: **your password**

Alternately, you can even follow the steps given in the below URL,  to install docker and azure studio 👇
[https://www.quackit.com/sql_server/mac/install_sql_server_on_a_mac.cfm](https://www.quackit.com/sql_server/mac/install_sql_server_on_a_mac.cfm)


# Steps to install package manager console on Mac


Visual Studio >> Extensions -> install nuget package manager console extensions

Restart Visual studio

View - Nuget Package manager console. 



