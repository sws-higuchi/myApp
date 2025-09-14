FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

COPY ./myApp/myApp/publish/ ./

ENTRYPOINT ["dotnet", "MyApp.dll"]
