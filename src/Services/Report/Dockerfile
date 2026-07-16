FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Report/HRMS.Services.Report.API/HRMS.Services.Report.API.csproj", "Services/Report/HRMS.Services.Report.API/"]
COPY ["src/Services/Report/HRMS.Services.Report.Application/HRMS.Services.Report.Application.csproj", "Services/Report/HRMS.Services.Report.Application/"]
COPY ["src/Services/Report/HRMS.Services.Report.Infrastructure/HRMS.Services.Report.Infrastructure.csproj", "Services/Report/HRMS.Services.Report.Infrastructure/"]
COPY ["src/Services/Report/HRMS.Services.Report.Domain/HRMS.Services.Report.Domain.csproj", "Services/Report/HRMS.Services.Report.Domain/"]
COPY ["src/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/HRMS.Shared.Kernel/"]
RUN dotnet restore "Services/Report/HRMS.Services.Report.API/HRMS.Services.Report.API.csproj"

COPY src/Services/Report/ src/Services/Report/
COPY src/Shared/HRMS.Shared.Kernel/ src/Shared/HRMS.Shared.Kernel/
WORKDIR "/src/Services/Report/HRMS.Services.Report.API"
RUN dotnet build "HRMS.Services.Report.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HRMS.Services.Report.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.Services.Report.API.dll"]
