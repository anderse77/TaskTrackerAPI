# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build 
WORKDIR /src/TaskTracker.Api

# For better caching
COPY src/TaskTracker.Api/TaskTracker.Api.csproj .
RUN dotnet restore

COPY src/TaskTracker.Api/ .
RUN dotnet publish -c Release -o /app

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "TaskTracker.Api.dll"]