FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/Services/Payroll/HRMS.Services.Payroll.API/HRMS.Services.Payroll.API.csproj", "Services/Payroll/HRMS.Services.Payroll.API/"]
COPY ["src/Services/Payroll/HRMS.Services.Payroll.Application/HRMS.Services.Payroll.Application.csproj", "Services/Payroll/HRMS.Services.Payroll.Application/"]
COPY ["src/Services/Payroll/HRMS.Services.Payroll.Domain/HRMS.Services.Payroll.Domain.csproj", "Services/Payroll/HRMS.Services.Payroll.Domain/"]
COPY ["src/Services/Payroll/HRMS.Services.Payroll.Infrastructure/HRMS.Services.Payroll.Infrastructure.csproj", "Services/Payroll/HRMS.Services.Payroll.Infrastructure/"]
COPY ["src/Shared/Kernel/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/Kernel/HRMS.Shared.Kernel/"]

RUN dotnet restore "Services/Payroll/HRMS.Services.Payroll.API/HRMS.Services.Payroll.API.csproj"

COPY . .

WORKDIR "/src/Services/Payroll/HRMS.Services.Payroll.API"
RUN dotnet build "HRMS.Services.Payroll.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HRMS.Services.Payroll.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "HRMS.Services.Payroll.API.dll"]
