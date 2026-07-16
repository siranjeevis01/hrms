FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Helpdesk/HRMS.Services.Helpdesk.API/HRMS.Services.Helpdesk.API.csproj", "Services/Helpdesk/HRMS.Services.Helpdesk.API/"]
COPY ["src/Services/Helpdesk/HRMS.Services.Helpdesk.Application/HRMS.Services.Helpdesk.Application.csproj", "Services/Helpdesk/HRMS.Services.Helpdesk.Application/"]
COPY ["src/Services/Helpdesk/HRMS.Services.Helpdesk.Infrastructure/HRMS.Services.Helpdesk.Infrastructure.csproj", "Services/Helpdesk/HRMS.Services.Helpdesk.Infrastructure/"]
COPY ["src/Services/Helpdesk/HRMS.Services.Helpdesk.Domain/HRMS.Services.Helpdesk.Domain.csproj", "Services/Helpdesk/HRMS.Services.Helpdesk.Domain/"]
COPY ["src/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/HRMS.Shared.Kernel/"]
RUN dotnet restore "Services/Helpdesk/HRMS.Services.Helpdesk.API/HRMS.Services.Helpdesk.API.csproj"

COPY src/Services/Helpdesk/ src/Services/Helpdesk/
COPY src/Shared/HRMS.Shared.Kernel/ src/Shared/HRMS.Shared.Kernel/
WORKDIR "/src/Services/Helpdesk/HRMS.Services.Helpdesk.API"
RUN dotnet build "HRMS.Services.Helpdesk.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HRMS.Services.Helpdesk.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.Services.Helpdesk.API.dll"]
