FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Expense/HRMS.Services.Expense.API/HRMS.Services.Expense.API.csproj", "Services/Expense/HRMS.Services.Expense.API/"]
COPY ["src/Services/Expense/HRMS.Services.Expense.Application/HRMS.Services.Expense.Application.csproj", "Services/Expense/HRMS.Services.Expense.Application/"]
COPY ["src/Services/Expense/HRMS.Services.Expense.Infrastructure/HRMS.Services.Expense.Infrastructure.csproj", "Services/Expense/HRMS.Services.Expense.Infrastructure/"]
COPY ["src/Services/Expense/HRMS.Services.Expense.Domain/HRMS.Services.Expense.Domain.csproj", "Services/Expense/HRMS.Services.Expense.Domain/"]
COPY ["src/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/HRMS.Shared.Kernel/"]
RUN dotnet restore "Services/Expense/HRMS.Services.Expense.API/HRMS.Services.Expense.API.csproj"

COPY src/Services/Expense/ src/Services/Expense/
COPY src/Shared/HRMS.Shared.Kernel/ src/Shared/HRMS.Shared.Kernel/
WORKDIR "/src/Services/Expense/HRMS.Services.Expense.API"
RUN dotnet build "HRMS.Services.Expense.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HRMS.Services.Expense.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.Services.Expense.API.dll"]
