FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Performance/HRMS.Services.Performance.API/HRMS.Services.Performance.API.csproj", "Services/Performance/HRMS.Services.Performance.API/"]
COPY ["src/Services/Performance/HRMS.Services.Performance.Application/HRMS.Services.Performance.Application.csproj", "Services/Performance/HRMS.Services.Performance.Application/"]
COPY ["src/Services/Performance/HRMS.Services.Performance.Infrastructure/HRMS.Services.Performance.Infrastructure.csproj", "Services/Performance/HRMS.Services.Performance.Infrastructure/"]
COPY ["src/Services/Performance/HRMS.Services.Performance.Domain/HRMS.Services.Performance.Domain.csproj", "Services/Performance/HRMS.Services.Performance.Domain/"]
COPY ["src/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/HRMS.Shared.Kernel/"]
RUN dotnet restore "Services/Performance/HRMS.Services.Performance.API/HRMS.Services.Performance.API.csproj"

COPY src/Services/Performance/ src/Services/Performance/
COPY src/Shared/HRMS.Shared.Kernel/ src/Shared/HRMS.Shared.Kernel/
WORKDIR "/src/Services/Performance/HRMS.Services.Performance.API"
RUN dotnet build "HRMS.Services.Performance.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HRMS.Services.Performance.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.Services.Performance.API.dll"]
