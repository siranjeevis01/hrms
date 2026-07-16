FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/ProjectTask/HRMS.Services.ProjectTask.API/HRMS.Services.ProjectTask.API.csproj", "Services/ProjectTask/HRMS.Services.ProjectTask.API/"]
COPY ["src/Services/ProjectTask/HRMS.Services.ProjectTask.Application/HRMS.Services.ProjectTask.Application.csproj", "Services/ProjectTask/HRMS.Services.ProjectTask.Application/"]
COPY ["src/Services/ProjectTask/HRMS.Services.ProjectTask.Infrastructure/HRMS.Services.ProjectTask.Infrastructure.csproj", "Services/ProjectTask/HRMS.Services.ProjectTask.Infrastructure/"]
COPY ["src/Services/ProjectTask/HRMS.Services.ProjectTask.Domain/HRMS.Services.ProjectTask.Domain.csproj", "Services/ProjectTask/HRMS.Services.ProjectTask.Domain/"]
COPY ["src/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/HRMS.Shared.Kernel/"]
RUN dotnet restore "Services/ProjectTask/HRMS.Services.ProjectTask.API/HRMS.Services.ProjectTask.API.csproj"

COPY src/Services/ProjectTask/ src/Services/ProjectTask/
COPY src/Shared/HRMS.Shared.Kernel/ src/Shared/HRMS.Shared.Kernel/
WORKDIR "/src/Services/ProjectTask/HRMS.Services.ProjectTask.API"
RUN dotnet build "HRMS.Services.ProjectTask.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HRMS.Services.ProjectTask.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.Services.ProjectTask.API.dll"]
