FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/CineControl.BookingService.API/CineControl.BookingService.API.csproj", "src/CineControl.BookingService.API/"]
COPY ["src/Common/CineControl.Common/CineControl.Common.csproj", "src/Common/CineControl.Common/"]
COPY ["src/Common/CineControl.Common.Clients.IdentityService/CineControl.Common.Clients.IdentityService.csproj" \
, "src/Common/CineControl.Common.Clients.IdentityService/"]
COPY ["src/Common/CineControl.Common.ServiceDefaults/CineControl.Common.ServiceDefaults.csproj","src/Common/CineControl.Common.ServiceDefaults/"]
RUN dotnet restore "src/CineControl.BookingService.API/CineControl.BookingService.API.csproj"
COPY . .
WORKDIR "/src/src/CineControl.BookingService.API"
RUN dotnet build "CineControl.BookingService.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "CineControl.BookingService.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CineControl.BookingService.API.dll"]
