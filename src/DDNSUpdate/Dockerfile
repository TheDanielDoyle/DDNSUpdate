ARG APP_BUILD_RUNTIME=linux-x64
ARG APP_BUILD_VERSION=1.0.0.0
ARG DOCKER_BUILD_RUNTIME=mcr.microsoft.com/dotnet/runtime:5.0-buster-slim
ARG DOCKER_BUILD_SDK=mcr.microsoft.com/dotnet/sdk:5.0

FROM $DOCKER_BUILD_RUNTIME AS base
WORKDIR /app

FROM $DOCKER_BUILD_SDK AS build
ARG APP_BUILD_RUNTIME
ARG APP_BUILD_VERSION

WORKDIR /src
COPY ["DDNSUpdate.csproj", "DDNSUpdate/"]
RUN dotnet restore "DDNSUpdate/DDNSUpdate.csproj"
COPY . .
RUN dotnet build --runtime $APP_BUILD_RUNTIME -p:Version=$APP_BUILD_VERSION "DDNSUpdate.csproj" -c Release -o /app/build

FROM build AS publish
ARG APP_BUILD_RUNTIME
ARG APP_BUILD_VERSION

RUN dotnet publish --runtime $APP_BUILD_RUNTIME -p:Version=$APP_BUILD_VERSION "DDNSUpdate.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DDNSUpdate.dll"]
