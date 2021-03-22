# CalculAPI

The CalculAPI is created in .NET 5
To use it, you have to install Visual Studio 2019 and .NET 5 in your computer

This project has 4 parts:
- Server
- Client
- Shared
- Tests

## Server
This project is a REST web api and it has the operations of our calculator.
Also, has a log about all the operations that the users do, and a log with all the things that happend during execution.

## Client
This is a console project.
This project has a menu options and it is who call the server with the corresponding parameters to get the response with the results.

## Shared
This projec has the shared objects that Server and Client use.

## Tests
This project has the test of the Server functions.

## How to use
First of all, we have to download all the projects in our computer.<br/>
Then, we have to execute the server project.<br/>
When the server project will be running, we have to execute the client project.<br/>
Then we will see the menu in a Windows console.<br/>
The menu has a part where we can see all the operations that one client has done (Query option). This log, will only restart when the server is restarted. If you only restart the client, the data will continue in server, and you can see again the logs of previus client executions.<br/>
