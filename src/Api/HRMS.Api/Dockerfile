FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Api/HRMS.Api/HRMS.Api.csproj", "src/Api/HRMS.Api/"]
COPY ["src/Shared/HRMS.Shared.Kernel/HRMS.Shared.Kernel.csproj", "src/Shared/HRMS.Shared.Kernel/"]
COPY ["src/Shared/HRMS.Shared.Persistence/HRMS.Shared.Persistence.csproj", "src/Shared/HRMS.Shared.Persistence/"]
COPY ["src/Shared/HRMS.Shared.Security/HRMS.Shared.Security.csproj", "src/Shared/HRMS.Shared.Security/"]
COPY ["src/Shared/HRMS.Shared.Caching/HRMS.Shared.Caching.csproj", "src/Shared/HRMS.Shared.Caching/"]
COPY ["src/Shared/HRMS.Shared.Messaging/HRMS.Shared.Messaging.csproj", "src/Shared/HRMS.Shared.Messaging/"]
COPY ["src/Shared/HRMS.Shared.Observability/HRMS.Shared.Observability.csproj", "src/Shared/HRMS.Shared.Observability/"]

COPY ["src/Services/Attendance/HRMS.Services.Attendance.Domain/HRMS.Services.Attendance.Domain.csproj", "src/Services/Attendance/HRMS.Services.Attendance.Domain/"]
COPY ["src/Services/Attendance/HRMS.Services.Attendance.Application/HRMS.Services.Attendance.Application.csproj", "src/Services/Attendance/HRMS.Services.Attendance.Application/"]
COPY ["src/Services/Attendance/HRMS.Services.Attendance.Infrastructure/HRMS.Services.Attendance.Infrastructure.csproj", "src/Services/Attendance/HRMS.Services.Attendance.Infrastructure/"]
COPY ["src/Services/Audit/HRMS.Services.Audit.Domain/HRMS.Services.Audit.Domain.csproj", "src/Services/Audit/HRMS.Services.Audit.Domain/"]
COPY ["src/Services/Audit/HRMS.Services.Audit.Application/HRMS.Services.Audit.Application.csproj", "src/Services/Audit/HRMS.Services.Audit.Application/"]
COPY ["src/Services/Audit/HRMS.Services.Audit.Infrastructure/HRMS.Services.Audit.Infrastructure.csproj", "src/Services/Audit/HRMS.Services.Audit.Infrastructure/"]
COPY ["src/Services/Chat/HRMS.Services.Chat.Domain/HRMS.Services.Chat.Domain.csproj", "src/Services/Chat/HRMS.Services.Chat.Domain/"]
COPY ["src/Services/Chat/HRMS.Services.Chat.Application/HRMS.Services.Chat.Application.csproj", "src/Services/Chat/HRMS.Services.Chat.Application/"]
COPY ["src/Services/Chat/HRMS.Services.Chat.Infrastructure/HRMS.Services.Chat.Infrastructure.csproj", "src/Services/Chat/HRMS.Services.Chat.Infrastructure/"]
COPY ["src/Services/Dashboard/HRMS.Services.Dashboard.Domain/HRMS.Services.Dashboard.Domain.csproj", "src/Services/Dashboard/HRMS.Services.Dashboard.Domain/"]
COPY ["src/Services/Dashboard/HRMS.Services.Dashboard.Application/HRMS.Services.Dashboard.Application.csproj", "src/Services/Dashboard/HRMS.Services.Dashboard.Application/"]
COPY ["src/Services/Dashboard/HRMS.Services.Dashboard.Infrastructure/HRMS.Services.Dashboard.Infrastructure.csproj", "src/Services/Dashboard/HRMS.Services.Dashboard.Infrastructure/"]
COPY ["src/Services/Document/HRMS.Services.Document.Domain/HRMS.Services.Document.Domain.csproj", "src/Services/Document/HRMS.Services.Document.Domain/"]
COPY ["src/Services/Document/HRMS.Services.Document.Application/HRMS.Services.Document.Application.csproj", "src/Services/Document/HRMS.Services.Document.Application/"]
COPY ["src/Services/Document/HRMS.Services.Document.Infrastructure/HRMS.Services.Document.Infrastructure.csproj", "src/Services/Document/HRMS.Services.Document.Infrastructure/"]
COPY ["src/Services/Employee/HRMS.Services.Employee.Domain/HRMS.Services.Employee.Domain.csproj", "src/Services/Employee/HRMS.Services.Employee.Domain/"]
COPY ["src/Services/Employee/HRMS.Services.Employee.Application/HRMS.Services.Employee.Application.csproj", "src/Services/Employee/HRMS.Services.Employee.Application/"]
COPY ["src/Services/Employee/HRMS.Services.Employee.Infrastructure/HRMS.Services.Employee.Infrastructure.csproj", "src/Services/Employee/HRMS.Services.Employee.Infrastructure/"]
COPY ["src/Services/Expense/HRMS.Services.Expense.Domain/HRMS.Services.Expense.Domain.csproj", "src/Services/Expense/HRMS.Services.Expense.Domain/"]
COPY ["src/Services/Expense/HRMS.Services.Expense.Application/HRMS.Services.Expense.Application.csproj", "src/Services/Expense/HRMS.Services.Expense.Application/"]
COPY ["src/Services/Expense/HRMS.Services.Expense.Infrastructure/HRMS.Services.Expense.Infrastructure.csproj", "src/Services/Expense/HRMS.Services.Expense.Infrastructure/"]
COPY ["src/Services/Helpdesk/HRMS.Services.Helpdesk.Domain/HRMS.Services.Helpdesk.Domain.csproj", "src/Services/Helpdesk/HRMS.Services.Helpdesk.Domain/"]
COPY ["src/Services/Helpdesk/HRMS.Services.Helpdesk.Application/HRMS.Services.Helpdesk.Application.csproj", "src/Services/Helpdesk/HRMS.Services.Helpdesk.Application/"]
COPY ["src/Services/Helpdesk/HRMS.Services.Helpdesk.Infrastructure/HRMS.Services.Helpdesk.Infrastructure.csproj", "src/Services/Helpdesk/HRMS.Services.Helpdesk.Infrastructure/"]
COPY ["src/Services/Identity/HRMS.Services.Identity.Domain/HRMS.Services.Identity.Domain.csproj", "src/Services/Identity/HRMS.Services.Identity.Domain/"]
COPY ["src/Services/Identity/HRMS.Services.Identity.Application/HRMS.Services.Identity.Application.csproj", "src/Services/Identity/HRMS.Services.Identity.Application/"]
COPY ["src/Services/Identity/HRMS.Services.Identity.Infrastructure/HRMS.Services.Identity.Infrastructure.csproj", "src/Services/Identity/HRMS.Services.Identity.Infrastructure/"]
COPY ["src/Services/Leave/Domain/HRMS.Services.Leave.Domain.csproj", "src/Services/Leave/Domain/"]
COPY ["src/Services/Leave/Application/HRMS.Services.Leave.Application.csproj", "src/Services/Leave/Application/"]
COPY ["src/Services/Leave/Infrastructure/HRMS.Services.Leave.Infrastructure.csproj", "src/Services/Leave/Infrastructure/"]
COPY ["src/Services/Notification/HRMS.Services.Notification.Domain/HRMS.Services.Notification.Domain.csproj", "src/Services/Notification/HRMS.Services.Notification.Domain/"]
COPY ["src/Services/Notification/HRMS.Services.Notification.Application/HRMS.Services.Notification.Application.csproj", "src/Services/Notification/HRMS.Services.Notification.Application/"]
COPY ["src/Services/Notification/HRMS.Services.Notification.Infrastructure/HRMS.Services.Notification.Infrastructure.csproj", "src/Services/Notification/HRMS.Services.Notification.Infrastructure/"]
COPY ["src/Services/Organization/HRMS.Services.Organization.Domain/HRMS.Services.Organization.Domain.csproj", "src/Services/Organization/HRMS.Services.Organization.Domain/"]
COPY ["src/Services/Organization/HRMS.Services.Organization.Application/HRMS.Services.Organization.Application.csproj", "src/Services/Organization/HRMS.Services.Organization.Application/"]
COPY ["src/Services/Organization/HRMS.Services.Organization.Infrastructure/HRMS.Services.Organization.Infrastructure.csproj", "src/Services/Organization/HRMS.Services.Organization.Infrastructure/"]
COPY ["src/Services/Payroll/HRMS.Services.Payroll.Domain/HRMS.Services.Payroll.Domain.csproj", "src/Services/Payroll/HRMS.Services.Payroll.Domain/"]
COPY ["src/Services/Payroll/HRMS.Services.Payroll.Application/HRMS.Services.Payroll.Application.csproj", "src/Services/Payroll/HRMS.Services.Payroll.Application/"]
COPY ["src/Services/Payroll/HRMS.Services.Payroll.Infrastructure/HRMS.Services.Payroll.Infrastructure.csproj", "src/Services/Payroll/HRMS.Services.Payroll.Infrastructure/"]
COPY ["src/Services/Performance/HRMS.Services.Performance.Domain/HRMS.Services.Performance.Domain.csproj", "src/Services/Performance/HRMS.Services.Performance.Domain/"]
COPY ["src/Services/Performance/HRMS.Services.Performance.Application/HRMS.Services.Performance.Application.csproj", "src/Services/Performance/HRMS.Services.Performance.Application/"]
COPY ["src/Services/ProjectTask/HRMS.Services.ProjectTask.Domain/HRMS.Services.ProjectTask.Domain.csproj", "src/Services/ProjectTask/HRMS.Services.ProjectTask.Domain/"]
COPY ["src/Services/ProjectTask/HRMS.Services.ProjectTask.Application/HRMS.Services.ProjectTask.Application.csproj", "src/Services/ProjectTask/HRMS.Services.ProjectTask.Application/"]
COPY ["src/Services/ProjectTask/HRMS.Services.ProjectTask.Infrastructure/HRMS.Services.ProjectTask.Infrastructure.csproj", "src/Services/ProjectTask/HRMS.Services.ProjectTask.Infrastructure/"]
COPY ["src/Services/Recruitment/HRMS.Services.Recruitment.Domain/HRMS.Services.Recruitment.Domain.csproj", "src/Services/Recruitment/HRMS.Services.Recruitment.Domain/"]
COPY ["src/Services/Recruitment/HRMS.Services.Recruitment.Application/HRMS.Services.Recruitment.Application.csproj", "src/Services/Recruitment/HRMS.Services.Recruitment.Application/"]
COPY ["src/Services/Recruitment/HRMS.Services.Recruitment.Infrastructure/HRMS.Services.Recruitment.Infrastructure.csproj", "src/Services/Recruitment/HRMS.Services.Recruitment.Infrastructure/"]
COPY ["src/Services/Report/HRMS.Services.Report.Domain/HRMS.Services.Report.Domain.csproj", "src/Services/Report/HRMS.Services.Report.Domain/"]
COPY ["src/Services/Report/HRMS.Services.Report.Application/HRMS.Services.Report.Application.csproj", "src/Services/Report/HRMS.Services.Report.Application/"]
COPY ["src/Services/Report/HRMS.Services.Report.Infrastructure/HRMS.Services.Report.Infrastructure.csproj", "src/Services/Report/HRMS.Services.Report.Infrastructure/"]
COPY ["src/Services/Training/HRMS.Services.Training.Domain/HRMS.Services.Training.Domain.csproj", "src/Services/Training/HRMS.Services.Training.Domain/"]
COPY ["src/Services/Training/HRMS.Services.Training.Application/HRMS.Services.Training.Application.csproj", "src/Services/Training/HRMS.Services.Training.Application/"]
COPY ["src/Services/Training/HRMS.Services.Training.Infrastructure/HRMS.Services.Training.Infrastructure.csproj", "src/Services/Training/HRMS.Services.Training.Infrastructure/"]
COPY ["src/Services/Travel/HRMS.Services.Travel.Domain/HRMS.Services.Travel.Domain.csproj", "src/Services/Travel/HRMS.Services.Travel.Domain/"]
COPY ["src/Services/Travel/HRMS.Services.Travel.Application/HRMS.Services.Travel.Application.csproj", "src/Services/Travel/HRMS.Services.Travel.Application/"]
COPY ["src/Services/Travel/HRMS.Services.Travel.Infrastructure/HRMS.Services.Travel.Infrastructure.csproj", "src/Services/Travel/HRMS.Services.Travel.Infrastructure/"]
COPY ["src/Services/Workflow/HRMS.Services.Workflow.Domain/HRMS.Services.Workflow.Domain.csproj", "src/Services/Workflow/HRMS.Services.Workflow.Domain/"]
COPY ["src/Services/Workflow/HRMS.Services.Workflow.Application/HRMS.Services.Workflow.Application.csproj", "src/Services/Workflow/HRMS.Services.Workflow.Application/"]

RUN dotnet restore "src/Api/HRMS.Api/HRMS.Api.csproj"

COPY src/ .
RUN dotnet publish "src/Api/HRMS.Api/HRMS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
RUN addgroup -S hrms && adduser -S hrms -G hrms
RUN apk add --no-cache curl
WORKDIR /app
COPY --from=build /app/publish .
USER hrms
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080
HEALTHCHECK --interval=30s --timeout=10s --start-period=60s --retries=3 CMD curl -f http://localhost:8080/health/live || exit 1
ENTRYPOINT ["dotnet", "HRMS.Api.dll"]
