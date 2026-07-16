FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

COPY ["src/Services/Identity/HRMS.Services.Identity.API/HRMS.Services.Identity.API.csproj", "src/Services/Identity/HRMS.Services.Identity.API/"]
COPY ["src/Services/Identity/HRMS.Services.Identity.Application/HRMS.Services.Identity.Application.csproj", "src/Services/Identity/HRMS.Services.Identity.Application/"]
COPY ["src/Services/Identity/HRMS.Services.Identity.Domain/HRMS.Services.Identity.Domain.csproj", "src/Services/Identity/HRMS.Services.Identity.Domain/"]
COPY ["src/Services/Identity/HRMS.Services.Identity.Infrastructure/HRMS.Services.Identity.Infrastructure.csproj", "src/Services/Identity/HRMS.Services.Identity.Infrastructure/"]
COPY ["src/Shared/Kernel/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "src/Shared/Kernel/HRMS.Shared.Kernel/"]
COPY ["src/Shared/Persistence/HRMS.Shared.Persistence/HRMS.Shared.Persistence.csproj", "src/Shared/Persistence/HRMS.Shared.Persistence/"]
COPY ["src/Shared/Caching/HRMS.Shared.Caching/HRMS.Shared.Caching.csproj", "src/Shared/Caching/HRMS.Shared.Caching/"]
COPY ["src/Shared/Security/HRMS.Shared.Security/HRMS.Shared.Security.csproj", "src/Shared/Security/HRMS.Shared.Security/"]
RUN dotnet restore "src/Services/Identity/HRMS.Services.Identity.API/HRMS.Services.Identity.API.csproj"

COPY src/ .
WORKDIR "/src/src/Services/Identity/HRMS.Services.Identity.API"
RUN dotnet build "HRMS.Services.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HRMS.Services.Identity.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS runtime
WORKDIR /app
EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:5001
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

RUN apk add --no-cache icu-libs

COPY --from=publish /app/publish .
COPY ["src/Services/Identity/HRMS.Services.Identity.API/appsettings.json", "."]

HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
    CMD wget --no-verbose --tries=1 --spider http://localhost:5001/health/live || exit 1

ENTRYPOINT ["dotnet", "HRMS.Services.Identity.API.dll"]
