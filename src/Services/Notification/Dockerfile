FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/Services/Notification/HRMS.Services.Notification.API/HRMS.Services.Notification.API.csproj", "Services/Notification/HRMS.Services.Notification.API/"]
COPY ["src/Services/Notification/HRMS.Services.Notification.Infrastructure/HRMS.Services.Notification.Infrastructure.csproj", "Services/Notification/HRMS.Services.Notification.Infrastructure/"]
COPY ["src/Services/Notification/HRMS.Services.Notification.Application/HRMS.Services.Notification.Application.csproj", "Services/Notification/HRMS.Services.Notification.Application/"]
COPY ["src/Services/Notification/HRMS.Services.Notification.Domain/HRMS.Services.Notification.Domain.csproj", "Services/Notification/HRMS.Services.Notification.Domain/"]
COPY ["src/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/HRMS.Shared.Kernel/"]
RUN dotnet restore "Services/Notification/HRMS.Services.Notification.API/HRMS.Services.Notification.API.csproj"

COPY src/Services/Notification/ src/Services/Notification/
COPY src/Shared/HRMS.Shared.Kernel/ src/Shared/HRMS.Shared.Kernel/

WORKDIR "/src/Services/Notification/HRMS.Services.Notification.API"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "HRMS.Services.Notification.API.dll"]
