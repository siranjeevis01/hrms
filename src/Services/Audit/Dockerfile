FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Audit/HRMS.Services.Audit.API/HRMS.Services.Audit.API.csproj", "Services/Audit/HRMS.Services.Audit.API/"]
COPY ["src/Services/Audit/HRMS.Services.Audit.Application/HRMS.Services.Audit.Application.csproj", "Services/Audit/HRMS.Services.Audit.Application/"]
COPY ["src/Services/Audit/HRMS.Services.Audit.Infrastructure/HRMS.Services.Audit.Infrastructure.csproj", "Services/Audit/HRMS.Services.Audit.Infrastructure/"]
COPY ["src/Services/Audit/HRMS.Services.Audit.Domain/HRMS.Services.Audit.Domain.csproj", "Services/Audit/HRMS.Services.Audit.Domain/"]
COPY ["src/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/HRMS.Shared.Kernel/"]
RUN dotnet restore "Services/Audit/HRMS.Services.Audit.API/HRMS.Services.Audit.API.csproj"

COPY src/Services/Audit/ src/Services/Audit/
COPY src/Shared/HRMS.Shared.Kernel/ src/Shared/HRMS.Shared.Kernel/
WORKDIR "/src/Services/Audit/HRMS.Services.Audit.API"
RUN dotnet build "HRMS.Services.Audit.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HRMS.Services.Audit.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.Services.Audit.API.dll"]
