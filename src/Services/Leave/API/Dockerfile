FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Services/Leave/API/HRMS.Services.Leave.API.csproj", "Services/Leave/API/"]
COPY ["Services/Leave/Application/HRMS.Services.Leave.Application.csproj", "Services/Leave/Application/"]
COPY ["Services/Leave/Domain/HRMS.Services.Leave.Domain.csproj", "Services/Leave/Domain/"]
COPY ["Services/Leave/Infrastructure/HRMS.Services.Leave.Infrastructure.csproj", "Services/Leave/Infrastructure/"]
COPY ["Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/HRMS.Shared.Kernel/"]
RUN dotnet restore "Services/Leave/API/HRMS.Services.Leave.API.csproj"

COPY . .
WORKDIR "/src/Services/Leave/API"
RUN dotnet build "HRMS.Services.Leave.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HRMS.Services.Leave.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.Services.Leave.API.dll"]
