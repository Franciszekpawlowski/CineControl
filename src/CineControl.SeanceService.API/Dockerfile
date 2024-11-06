FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/CineControl.SeanceService.API/CineControl.SeanceService.API.csproj", "src/CineControl.SeanceService.API/"]
RUN dotnet restore "src/CineControl.SeanceService.API/CineControl.SeanceService.API.csproj"
COPY . .
WORKDIR "/src/src/CineControl.SeanceService.API"
RUN dotnet build "CineControl.SeanceService.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "CineControl.SeanceService.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CineControl.SeanceService.API.dll"]
