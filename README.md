## Billable Hours
A web application that allows an outsourcing firm to upload a CSV of their employees' work shift and it generates invoices for the respective projects that each of the employees worked on as indicated on the CSV data.

## Tech/Framework used
- Written in C# using ASP.NET Core framework
- Microsoft SQL Server Database
- Docker support included.

## Installation
- Clone the repository
- The application connects to the database service set up in the docker-compose file.
- You can install a copy of MSSQL Server via docker https://hub.docker.com/_/microsoft-mssql-server or a normal DE installation via https://docs.microsoft.com/en-us/sql/database-engine/install-windows/install-sql-server?view=sql-server-2017
- If you have a local running instance of MSSQL Server, please modify the connection string in the `appSettings.Development.json` file to point to your instance.
- To use local app memory rather than database, change the value `UseDatabase` in `appSettings.Development.json` to `false`.

### Using Visual Studio
- Download Visual Studio 2017 and above. 
- For Mac users, Download Visual Studio Community Version 2019.
- Build and Run the app

### Using VS Code
- Download .NET Core tools https://dotnet.microsoft.com/download
- Run `dotnet build` on your terminal to build
- Run `dotnet run` on your termi

### Using Docker to build
- Install Docker Engine on your local machine from https://docs.docker.com/install/
- Run `docker-compose up` on your terminal to build the web app and the database services.
- Run `docker ps | grep billablehours` on your terminal to get the exposed port number. 
- Connect to the app on the exposed port number.

## Tests

### Using Visual Studio
- The IDE provides a way to run tests directly.

### Using VS Code
- Run `dotnet test` on your terminal in the test folder directory to test.

## How to use?
- Upload a CSV file containing the employee work shifts.
- Enter the positions of the respective csv columns
- To view a company's invoice as PDF, mark the checkbox `Generate PDF`. To view all generated invoices as a table, leave the checkbox unmarked 
- Mark the checkbox `First row is header` to allow the application skip the first row during processing.



MIT © [Xpacer]()
