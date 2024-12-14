FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/CineControl.OperatorPanel/CineControl.OperatorPanel.csproj", "src/CineControl.OperatorPanel/"]
RUN dotnet restore "src/CineControl.OperatorPanel/CineControl.OperatorPanel.csproj"
COPY . .
WORKDIR "/src/src/CineControl.OperatorPanel"
RUN dotnet build "CineControl.OperatorPanel.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "CineControl.OperatorPanel.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER root
RUN mkdir ./keys && chown app:app ./keys
USER app
ENTRYPOINT ["dotnet", "CineControl.OperatorPanel.dll"]
