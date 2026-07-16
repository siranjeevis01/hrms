FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Workflow/HRMS.Services.Workflow.API/HRMS.Services.Workflow.API.csproj", "Services/Workflow/HRMS.Services.Workflow.API/"]
COPY ["src/Services/Workflow/HRMS.Services.Workflow.Application/HRMS.Services.Workflow.Application.csproj", "Services/Workflow/HRMS.Services.Workflow.Application/"]
COPY ["src/Services/Workflow/HRMS.Services.Workflow.Infrastructure/HRMS.Services.Workflow.Infrastructure.csproj", "Services/Workflow/HRMS.Services.Workflow.Infrastructure/"]
COPY ["src/Services/Workflow/HRMS.Services.Workflow.Domain/HRMS.Services.Workflow.Domain.csproj", "Services/Workflow/HRMS.Services.Workflow.Domain/"]
COPY ["src/BuildingBlocks/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "BuildingBlocks/Shared/HRMS.Shared.Kernel/"]
RUN dotnet restore "Services/Workflow/HRMS.Services.Workflow.API/HRMS.Services.Workflow.API.csproj"

COPY src/Services/Workflow/ src/Services/Workflow/
COPY src/BuildingBlocks/Shared/HRMS.Shared.Kernel/ src/BuildingBlocks/Shared/HRMS.Shared.Kernel/
WORKDIR "/src/Services/Workflow/HRMS.Services.Workflow.API"
RUN dotnet build "HRMS.Services.Workflow.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HRMS.Services.Workflow.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.Services.Workflow.API.dll"]
