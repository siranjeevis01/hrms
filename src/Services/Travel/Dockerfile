FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Travel/HRMS.Services.Travel.API/HRMS.Services.Travel.API.csproj", "Services/Travel/HRMS.Services.Travel.API/"]
COPY ["src/Services/Travel/HRMS.Services.Travel.Application/HRMS.Services.Travel.Application.csproj", "Services/Travel/HRMS.Services.Travel.Application/"]
COPY ["src/Services/Travel/HRMS.Services.Travel.Infrastructure/HRMS.Services.Travel.Infrastructure.csproj", "Services/Travel/HRMS.Services.Travel.Infrastructure/"]
COPY ["src/Services/Travel/HRMS.Services.Travel.Domain/HRMS.Services.Travel.Domain.csproj", "Services/Travel/HRMS.Services.Travel.Domain/"]
COPY ["src/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/HRMS.Shared.Kernel/"]
RUN dotnet restore "Services/Travel/HRMS.Services.Travel.API/HRMS.Services.Travel.API.csproj"

COPY src/Services/Travel/ src/Services/Travel/
COPY src/Shared/HRMS.Shared.Kernel/ src/Shared/HRMS.Shared.Kernel/
WORKDIR "/src/Services/Travel/HRMS.Services.Travel.API"
RUN dotnet build "HRMS.Services.Travel.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HRMS.Services.Travel.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.Services.Travel.API.dll"]
