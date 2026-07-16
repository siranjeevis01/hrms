FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Organization/HRMS.Services.Organization.API/HRMS.Services.Organization.API.csproj", "Services/Organization/HRMS.Services.Organization.API/"]
COPY ["src/Services/Organization/HRMS.Services.Organization.Application/HRMS.Services.Organization.Application.csproj", "Services/Organization/HRMS.Services.Organization.Application/"]
COPY ["src/Services/Organization/HRMS.Services.Organization.Domain/HRMS.Services.Organization.Domain.csproj", "Services/Organization/HRMS.Services.Organization.Domain/"]
COPY ["src/Services/Organization/HRMS.Services.Organization.Infrastructure/HRMS.Services.Organization.Infrastructure.csproj", "Services/Organization/HRMS.Services.Organization.Infrastructure/"]
COPY ["src/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/HRMS.Shared.Kernel/"]
COPY ["src/Shared/HRMS.Shared.Persistence/HRMS.Shared.Persistence.csproj", "Shared/HRMS.Shared.Persistence/"]
COPY ["src/Shared/HRMS.Shared.Caching/HRMS.Shared.Caching.csproj", "Shared/HRMS.Shared.Caching/"]
RUN dotnet restore "Services/Organization/HRMS.Services.Organization.API/HRMS.Services.Organization.API.csproj"

COPY src/Services/Organization/HRMS.Services.Organization.API/ Services/Organization/HRMS.Services.Organization.API/
COPY src/Services/Organization/HRMS.Services.Organization.Application/ Services/Organization/HRMS.Services.Organization.Application/
COPY src/Services/Organization/HRMS.Services.Organization.Domain/ Services/Organization/HRMS.Services.Organization.Domain/
COPY src/Services/Organization/HRMS.Services.Organization.Infrastructure/ Services/Organization/HRMS.Services.Organization.Infrastructure/
COPY src/Shared/HRMS.Shared.Kernel/ Shared/HRMS.Shared.Kernel/
COPY src/Shared/HRMS.Shared.Persistence/ Shared/HRMS.Shared.Persistence/
COPY src/Shared/HRMS.Shared.Caching/ Shared/HRMS.Shared.Caching/

WORKDIR "/src/Services/Organization/HRMS.Services.Organization.API"
RUN dotnet build "HRMS.Services.Organization.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HRMS.Services.Organization.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.Services.Organization.API.dll"]
