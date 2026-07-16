FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Employee/HRMS.Services.Employee.API/HRMS.Services.Employee.API.csproj", "Services/Employee/HRMS.Services.Employee.API/"]
COPY ["src/Services/Employee/HRMS.Services.Employee.Application/HRMS.Services.Employee.Application.csproj", "Services/Employee/HRMS.Services.Employee.Application/"]
COPY ["src/Services/Employee/HRMS.Services.Employee.Infrastructure/HRMS.Services.Employee.Infrastructure.csproj", "Services/Employee/HRMS.Services.Employee.Infrastructure/"]
COPY ["src/Services/Employee/HRMS.Services.Employee.Domain/HRMS.Services.Employee.Domain.csproj", "Services/Employee/HRMS.Services.Employee.Domain/"]
COPY ["src/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/HRMS.Shared.Kernel/"]
RUN dotnet restore "Services/Employee/HRMS.Services.Employee.API/HRMS.Services.Employee.API.csproj"

COPY src/Services/Employee/ src/Services/Employee/
COPY src/Shared/HRMS.Shared.Kernel/ src/Shared/HRMS.Shared.Kernel/
WORKDIR "/src/Services/Employee/HRMS.Services.Employee.API"
RUN dotnet build "HRMS.Services.Employee.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HRMS.Services.Employee.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.Services.Employee.API.dll"]
