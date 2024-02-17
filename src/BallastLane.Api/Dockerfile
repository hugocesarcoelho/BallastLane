#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/BallastLane.Api/BallastLane.Api.csproj", "src/BallastLane.Api/"]
COPY ["src/BallastLane.ApplicationService/BallastLane.ApplicationService.csproj", "src/BallastLane.ApplicationService/"]
COPY ["src/BallastLane.Domain/BallastLane.Domain.csproj", "src/BallastLane.Domain/"]
COPY ["src/BallastLane.Infrastructure.Data/BallastLane.Infrastructure.Data.csproj", "src/BallastLane.Infrastructure.Data/"]
RUN dotnet restore "./src/BallastLane.Api/BallastLane.Api.csproj"
COPY . .
WORKDIR "/src/src/BallastLane.Api"
RUN dotnet build "./BallastLane.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BallastLane.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BallastLane.Api.dll"]