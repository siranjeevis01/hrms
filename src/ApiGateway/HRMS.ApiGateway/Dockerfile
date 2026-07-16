FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/ApiGateway/HRMS.ApiGateway/HRMS.ApiGateway.csproj", "src/ApiGateway/HRMS.ApiGateway/"]
RUN dotnet restore "src/ApiGateway/HRMS.ApiGateway/HRMS.ApiGateway.csproj"

COPY src/ApiGateway/HRMS.ApiGateway/ src/ApiGateway/HRMS.ApiGateway/
RUN dotnet build "src/ApiGateway/HRMS.ApiGateway/HRMS.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/ApiGateway/HRMS.ApiGateway/HRMS.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

RUN addgroup -g 1001 -S hrms && \
    adduser -S hrms -u 1001 -G hrms

COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS="http://0.0.0.0:5000;https://0.0.0.0:5001"
ENV ASPNETCORE_ENVIRONMENT="Production"
ENV DOTNET_gcServer=1
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

RUN apk add --no-cache \
    curl \
    ca-certificates \
    icu-libs \
    && rm -rf /var/cache/apk/*

USER hrms

HEALTHCHECK --interval=30s --timeout=10s --start-period=15s --retries=3 \
    CMD curl -f http://localhost:5000/health/live || exit 1

ENTRYPOINT ["dotnet", "HRMS.ApiGateway.dll"]
