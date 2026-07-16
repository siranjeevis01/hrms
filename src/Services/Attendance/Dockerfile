FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Attendance/HRMS.Services.Attendance.API/HRMS.Services.Attendance.API.csproj", "Services/Attendance/HRMS.Services.Attendance.API/"]
COPY ["src/Services/Attendance/HRMS.Services.Attendance.Application/HRMS.Services.Attendance.Application.csproj", "Services/Attendance/HRMS.Services.Attendance.Application/"]
COPY ["src/Services/Attendance/HRMS.Services.Attendance.Infrastructure/HRMS.Services.Attendance.Infrastructure.csproj", "Services/Attendance/HRMS.Services.Attendance.Infrastructure/"]
COPY ["src/Services/Attendance/HRMS.Services.Attendance.Domain/HRMS.Services.Attendance.Domain.csproj", "Services/Attendance/HRMS.Services.Attendance.Domain/"]
COPY ["src/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/HRMS.Shared.Kernel/"]
RUN dotnet restore "Services/Attendance/HRMS.Services.Attendance.API/HRMS.Services.Attendance.API.csproj"

COPY src/Services/Attendance/ src/Services/Attendance/
COPY src/Shared/HRMS.Shared.Kernel/ src/Shared/HRMS.Shared.Kernel/
WORKDIR "/src/Services/Attendance/HRMS.Services.Attendance.API"
RUN dotnet build "HRMS.Services.Attendance.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HRMS.Services.Attendance.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.Services.Attendance.API.dll"]
