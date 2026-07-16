FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY . .
RUN dotnet restore "src/Api/HRMS.Api/HRMS.Api.csproj"
RUN dotnet publish "src/Api/HRMS.Api/HRMS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
RUN addgroup -S hrms && adduser -S hrms -G hrms
RUN apk add --no-cache curl
WORKDIR /app
COPY --from=build /app/publish .
USER hrms
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080
HEALTHCHECK --interval=30s --timeout=10s --start-period=60s --retries=3 CMD curl -f http://localhost:8080/health/live || exit 1
ENTRYPOINT ["dotnet", "HRMS.Api.dll"]
