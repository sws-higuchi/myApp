FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000

COPY ./publish/ ./

ENTRYPOINT ["dotnet", "myApp.dll"]
