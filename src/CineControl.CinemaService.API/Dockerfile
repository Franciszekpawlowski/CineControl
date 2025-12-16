FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/CineControl.CinemaService.API/CineControl.CinemaService.API.csproj", "src/CineControl.CinemaService.API/"]
COPY ["src/Common/CineControl.Common/CineControl.Common.csproj", "src/Common/CineControl.Common/"]
COPY ["src/Common/CineControl.Common.Clients.IdentityService/CineControl.Common.Clients.IdentityService.csproj" \
, "src/Common/CineControl.Common.Clients.IdentityService/"]
COPY ["src/Common/CineControl.Common.ServiceDefaults/CineControl.Common.ServiceDefaults.csproj","src/Common/CineControl.Common.ServiceDefaults/"]
RUN dotnet restore "src/CineControl.CinemaService.API/CineControl.CinemaService.API.csproj"
COPY . .
WORKDIR "/src/src/CineControl.CinemaService.API"
RUN dotnet build "CineControl.CinemaService.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "CineControl.CinemaService.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CineControl.CinemaService.API.dll"]
