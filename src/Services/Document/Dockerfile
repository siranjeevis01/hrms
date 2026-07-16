FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Document/HRMS.Services.Document.API/HRMS.Services.Document.API.csproj", "Services/Document/HRMS.Services.Document.API/"]
COPY ["src/Services/Document/HRMS.Services.Document.Application/HRMS.Services.Document.Application.csproj", "Services/Document/HRMS.Services.Document.Application/"]
COPY ["src/Services/Document/HRMS.Services.Document.Infrastructure/HRMS.Services.Document.Infrastructure.csproj", "Services/Document/HRMS.Services.Document.Infrastructure/"]
COPY ["src/Services/Document/HRMS.Services.Document.Domain/HRMS.Services.Document.Domain.csproj", "Services/Document/HRMS.Services.Document.Domain/"]
COPY ["src/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/HRMS.Shared.Kernel/"]
RUN dotnet restore "Services/Document/HRMS.Services.Document.API/HRMS.Services.Document.API.csproj"

COPY src/Services/Document/ src/Services/Document/
COPY src/Shared/HRMS.Shared.Kernel/ src/Shared/HRMS.Shared.Kernel/
WORKDIR "/src/Services/Document/HRMS.Services.Document.API"
RUN dotnet build "HRMS.Services.Document.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HRMS.Services.Document.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.Services.Document.API.dll"]
