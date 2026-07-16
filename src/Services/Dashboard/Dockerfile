FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Dashboard/HRMS.Services.Dashboard.API/HRMS.Services.Dashboard.API.csproj", "Services/Dashboard/HRMS.Services.Dashboard.API/"]
COPY ["src/Services/Dashboard/HRMS.Services.Dashboard.Application/HRMS.Services.Dashboard.Application.csproj", "Services/Dashboard/HRMS.Services.Dashboard.Application/"]
COPY ["src/Services/Dashboard/HRMS.Services.Dashboard.Infrastructure/HRMS.Services.Dashboard.Infrastructure.csproj", "Services/Dashboard/HRMS.Services.Dashboard.Infrastructure/"]
COPY ["src/Services/Dashboard/HRMS.Services.Dashboard.Domain/HRMS.Services.Dashboard.Domain.csproj", "Services/Dashboard/HRMS.Services.Dashboard.Domain/"]
COPY ["src/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/HRMS.Shared.Kernel/"]
RUN dotnet restore "Services/Dashboard/HRMS.Services.Dashboard.API/HRMS.Services.Dashboard.API.csproj"

COPY src/Services/Dashboard/ src/Services/Dashboard/
COPY src/Shared/HRMS.Shared.Kernel/ src/Shared/HRMS.Shared.Kernel/
WORKDIR "/src/Services/Dashboard/HRMS.Services.Dashboard.API"
RUN dotnet build "HRMS.Services.Dashboard.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HRMS.Services.Dashboard.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.Services.Dashboard.API.dll"]
