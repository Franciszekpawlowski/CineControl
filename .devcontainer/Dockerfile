FROM mcr.microsoft.com/devcontainers/dotnet:1-8.0-bookworm

# [Optional] Uncomment this section to install additional OS packages.
RUN dotnet tool install --global dotnet-ef && \
    dotnet tool install --global dotnet-aspnet-codegenerator 

RUN apt-get update && export DEBIAN_FRONTEND=noninteractive \
    && apt-get -y install --no-install-recommends postgresql-client \
    && apt clean

# [Optional] Uncomment this line to install global node packages.
# RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g <your-package-here>" 2>&1