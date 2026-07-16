FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Recruitment/HRMS.Services.Recruitment.API/HRMS.Services.Recruitment.API.csproj", "Services/Recruitment/HRMS.Services.Recruitment.API/"]
COPY ["src/Services/Recruitment/HRMS.Services.Recruitment.Application/HRMS.Services.Recruitment.Application.csproj", "Services/Recruitment/HRMS.Services.Recruitment.Application/"]
COPY ["src/Services/Recruitment/HRMS.Services.Recruitment.Infrastructure/HRMS.Services.Recruitment.Infrastructure.csproj", "Services/Recruitment/HRMS.Services.Recruitment.Infrastructure/"]
COPY ["src/Services/Recruitment/HRMS.Services.Recruitment.Domain/HRMS.Services.Recruitment.Domain.csproj", "Services/Recruitment/HRMS.Services.Recruitment.Domain/"]
COPY ["src/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/HRMS.Shared.Kernel/"]
RUN dotnet restore "Services/Recruitment/HRMS.Services.Recruitment.API/HRMS.Services.Recruitment.API.csproj"

COPY src/Services/Recruitment/ src/Services/Recruitment/
COPY src/Shared/HRMS.Shared.Kernel/ src/Shared/HRMS.Shared.Kernel/
WORKDIR "/src/Services/Recruitment/HRMS.Services.Recruitment.API"
RUN dotnet build "HRMS.Services.Recruitment.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HRMS.Services.Recruitment.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.Services.Recruitment.API.dll"]
