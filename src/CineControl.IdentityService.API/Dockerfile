FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/CineControl.IdentityService.API/CineControl.IdentityService.API.csproj", "src/CineControl.IdentityService.API/"]
RUN dotnet restore "src/CineControl.IdentityService.API/CineControl.IdentityService.API.csproj"
COPY . .
WORKDIR "/src/src/CineControl.IdentityService.API"
RUN dotnet build "CineControl.IdentityService.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "CineControl.IdentityService.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CineControl.IdentityService.API.dll"]
