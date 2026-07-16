FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Chat/HRMS.Services.Chat.API/HRMS.Services.Chat.API.csproj", "Services/Chat/HRMS.Services.Chat.API/"]
COPY ["src/Services/Chat/HRMS.Services.Chat.Application/HRMS.Services.Chat.Application.csproj", "Services/Chat/HRMS.Services.Chat.Application/"]
COPY ["src/Services/Chat/HRMS.Services.Chat.Infrastructure/HRMS.Services.Chat.Infrastructure.csproj", "Services/Chat/HRMS.Services.Chat.Infrastructure/"]
COPY ["src/Services/Chat/HRMS.Services.Chat.Domain/HRMS.Services.Chat.Domain.csproj", "Services/Chat/HRMS.Services.Chat.Domain/"]
COPY ["src/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "Shared/HRMS.Shared.Kernel/"]
RUN dotnet restore "Services/Chat/HRMS.Services.Chat.API/HRMS.Services.Chat.API.csproj"

COPY src/Services/Chat/ src/Services/Chat/
COPY src/Shared/HRMS.Shared.Kernel/ src/Shared/HRMS.Shared.Kernel/
WORKDIR "/src/Services/Chat/HRMS.Services.Chat.API"
RUN dotnet build "HRMS.Services.Chat.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HRMS.Services.Chat.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.Services.Chat.API.dll"]
