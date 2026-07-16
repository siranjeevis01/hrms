FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Training/HRMS.Services.Training.API/HRMS.Services.Training.API.csproj", "Services/Training/HRMS.Services.Training.API/"]
COPY ["src/Services/Training/HRMS.Services.Training.Application/HRMS.Services.Training.Application.csproj", "Services/Training/HRMS.Services.Training.Application/"]
COPY ["src/Services/Training/HRMS.Services.Training.Infrastructure/HRMS.Services.Training.Infrastructure.csproj", "Services/Training/HRMS.Services.Training.Infrastructure/"]
COPY ["src/Services/Training/HRMS.Services.Training.Domain/HRMS.Services.Training.Domain.csproj", "Services/Training/HRMS.Services.Training.Domain/"]
COPY ["src/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/HRMS.Shared.Kernel/"]
RUN dotnet restore "Services/Training/HRMS.Services.Training.API/HRMS.Services.Training.API.csproj"

COPY src/Services/Training/ src/Services/Training/
COPY src/Shared/HRMS.Shared.Kernel/ src/Shared/HRMS.Shared.Kernel/
WORKDIR "/src/Services/Training/HRMS.Services.Training.API"
RUN dotnet build "HRMS.Services.Training.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HRMS.Services.Training.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.Services.Training.API.dll"]
