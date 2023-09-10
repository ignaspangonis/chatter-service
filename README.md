# chatter-service

The backend part of a simple chat app with weather API, built with ASP.NET Core (C#) framework.

Check the frontend part [here](https://github.com/ignaspangonis/chatter). That repository contains the screenshots and screen recordings of the app and lists out the potential improvements for the app.

## Prerequisites

- Make sure you have mongodb installed ([installation guide](https://www.mongodb.com/docs/manual/administration/install-community/)). Once installed, run the DB. For example, on macOS, run `brew services start mongodb-community@6.0` to start the DB.

## How To Run
If you are using Visual studio IDE:
- Open the `ChatterService.sln` file in Visual Studio
- Set `ChatterService` project as Startup Project and build the project.
- Run the application.

Otherwise, use [dotnet run](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run) command to run the project.