ARG RUNTIMEIMAGE=mcr.microsoft.com/dotnet/runtime:7.0
ARG SDKIMAGE=mcr.microsoft.com/dotnet/sdk:7.0.302
ARG VERSION=1.0.0

FROM $RUNTIMEIMAGE AS base
WORKDIR /app

FROM --platform=$BUILDPLATFORM $SDKIMAGE AS build
ARG TARGETARCH
ARG VERSION
WORKDIR /src
COPY ["DDNSUpdate.csproj", "DDNSUpdate/"]
RUN dotnet restore -a $TARGETARCH "DDNSUpdate/DDNSUpdate.csproj"
COPY . .
RUN dotnet build -a $TARGETARCH "DDNSUpdate.csproj" -c Release -o /app/build -p:"Version=$VERSION"

FROM build AS publish
ARG TARGETARCH
ARG VERSION
RUN dotnet publish -a $TARGETARCH "DDNSUpdate.csproj" -c Release -o /app/publish -p:"Version=$VERSION"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DDNSUpdate.dll"]
