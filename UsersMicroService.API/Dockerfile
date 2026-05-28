# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8082
EXPOSE 8083


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UsersMicroService.API/UsersMicroService.API.csproj", "UsersMicroService.API/"]
COPY ["UserMicroService.Core/UsersMicroService.Core.csproj", "UsersMicroService.Core/"]
COPY ["UsersMicroService.Infrastructure/UsersMicroService.Infrastructure.csproj", "UsersMicroService.Infrastructure/"]

RUN dotnet restore "./UsersMicroService.API/UsersMicroService.API.csproj"
COPY . .
WORKDIR "/src/UsersMicroService.API"
RUN dotnet build "./UsersMicroService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UsersMicroService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV SQL_SERVER_HOST=***
ENV SQL_DB=**
ENV SQL_USER=***
ENV SQL_PASSWORD=***
ENTRYPOINT ["dotnet", "UsersMicroService.API.dll"]