ARG RUNTIMEIMAGE=mcr.microsoft.com/dotnet/runtime:9.0
ARG SDKIMAGE=mcr.microsoft.com/dotnet/sdk:9.0
ARG VERSION=1.0.0

USER $APP_UID

FROM $RUNTIMEIMAGE AS base
WORKDIR /app

FROM --platform=$BUILDPLATFORM $SDKIMAGE AS build
ARG TARGETARCH
ARG VERSION
WORKDIR /src
COPY ["DDNSUpdate.csproj", "DDNSUpdate/"]
RUN dotnet restore --arch $TARGETARCH "DDNSUpdate/DDNSUpdate.csproj"
COPY . .
RUN dotnet build --arch $TARGETARCH "DDNSUpdate.csproj" --configuration Release --output /app/build -p:"Version=$VERSION"

FROM build AS publish
ARG TARGETARCH
ARG VERSION
RUN dotnet publish --arch$TARGETARCH "DDNSUpdate.csproj" --no-self-contained --configuration Release --output /app/publish -p:"Version=$BUILD_VERSION;UseAppHost=false"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DDNSUpdate.dll"]
