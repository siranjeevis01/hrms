# HRMS Pro

Enterprise Human Resource Management System with 20 microservices, Angular 20 frontend, and .NET 9 backend.

## Architecture

```
┌─────────────────────────────────────────────────────┐
│                  Angular 20 Frontend                 │
│           Tailwind CSS · Angular Material            │
└───────────────────────┬─────────────────────────────┘
                        │ REST + SignalR
┌───────────────────────▼─────────────────────────────┐
│              .NET 9 Monolith API (HRMS.Api)          │
│  ┌─────────┐ ┌──────────┐ ┌──────────┐ ┌─────────┐ │
│  │Identity │ │Employee  │ │Leave     │ │Payroll  │ │
│  │Org      │ │Attendance│ │Expense   │ │Helpdesk │ │
│  │Project  │ │Perform.  │ │Training  │ │Chat     │ │
│  │Document │ │Recruit.  │ │Travel    │ │Workflow │ │
│  │Report   │ │Dashboard │ │Notif.    │ │Audit    │ │
│  └─────────┘ └──────────┘ └──────────┘ └─────────┘ │
└───────────────────────┬─────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────┐
│                    PostgreSQL                        │
└─────────────────────────────────────────────────────┘
```

**Deployment modes:**
- **Monolith** — single API process, single database (default, used on Render)
- **Microservices** — 20 independent services, per-service databases (Docker Compose)

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Frontend | Angular 20, TypeScript, Tailwind CSS, Angular Material, NgRx Signals |
| Backend | .NET 9, C#, Clean Architecture, MediatR, FluentValidation |
| Database | PostgreSQL (monolith), MySQL (microservices) |
| Auth | JWT Bearer, Firebase Auth (optional), TOTP MFA |
| Real-time | SignalR |
| Email | SMTP (Gmail, Brevo, any provider) |
| Caching | Redis |
| Messaging | RabbitMQ (microservices) |
| Observability | Serilog, Seq, OpenTelemetry, Jaeger |
| CI/CD | GitHub Actions |
| Deploy | Render (free tier), Docker, Docker Compose |

## Quick Start

### Local Development (Monolith)

```bash
# 1. Clone
git clone https://github.com/siranjeevis01/hrms.git
cd hrms

# 2. Start PostgreSQL (Docker)
docker run -d --name hrms-pg -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=hrms -p 5432:5432 postgres:16

# 3. Seed database
psql -h localhost -U postgres -d hrms -f scripts/seed-postgresql.sql

# 4. Start API
cd src/Api/HRMS.Api
dotnet run

# 5. Start Frontend (new terminal)
cd frontend
npm install
ng serve
```

API runs at `http://localhost:8080` · Swagger at `http://localhost:8080/swagger` · Frontend at `http://localhost:4200`

### Default Login

| Email | Password | Role |
|-------|----------|------|
| admin@hrms-pro.com | Admin@123 | SuperAdmin |

### Docker Compose (Monolith)

```bash
docker compose -f docker-compose.monolith.yml up -d
```

### Docker Compose (Microservices)

```bash
cp .env.example .env    # edit with your values
docker compose up -d
```

## Deploy to Render

1. Push to GitHub
2. Go to [render.com](https://render.com) → New → Blueprint
3. Select your repo — Render reads `render.yaml` automatically
4. Set environment variables (SMTP credentials for email)
5. After deploy, run the seed script against the provisioned PostgreSQL

The `render.yaml` provisions:
- **hrms-api** — .NET 9 web service (free tier)
- **hrms-db** — PostgreSQL database (free tier)
- **hrms-frontend** — Static site with Angular build

## Project Structure

```
HRMS/
├── src/
│   ├── Api/HRMS.Api/              # Monolith API (combines all services)
│   ├── Shared/                     # Cross-cutting concerns
│   │   ├── HRMS.Shared.Kernel/     # Domain events, interfaces, base classes
│   │   ├── HRMS.Shared.Persistence/ # EF Core, DbContext, interceptors
│   │   ├── HRMS.Shared.Security/   # JWT, password hashing
│   │   ├── HRMS.Shared.Caching/    # Redis
│   │   ├── HRMS.Shared.Messaging/  # RabbitMQ
│   │   └── HRMS.Shared.Observability/ # OpenTelemetry
│   └── Services/                   # 20 domain services
│       ├── Identity/               # Auth, users, roles, MFA
│       ├── Employee/               # Employee profiles, documents
│       ├── Organization/           # Companies, departments, branches
│       ├── Attendance/             # Clock-in, geo-fence, shifts
│       ├── Leave/                  # Leave types, balances, approvals
│       ├── Payroll/                # Salary, deductions, loans
│       ├── Expense/                # Claims, approvals, policies
│       ├── Recruitment/            # Jobs, candidates, interviews
│       ├── Training/               # Courses, enrollments, certs
│       ├── Performance/            # Reviews, goals, KPIs, OKRs
│       ├── ProjectTask/            # Projects, sprints, tasks, bugs
│       ├── Helpdesk/               # Tickets, knowledge base, FAQs
│       ├── Chat/                   # Messages, channels, presence
│       ├── Document/               # Upload, version, share
│       ├── Notification/           # Email, SMS, push, templates
│       ├── Workflow/               # Approval workflows, delegation
│       ├── Report/                 # Templates, scheduling
│       ├── Dashboard/              # Widgets, analytics
│       ├── Travel/                 # Requests, itineraries, visas
│       ├── Audit/                  # Logs, trails, data changes
│       └── Workflow/               # Process automation
├── frontend/                       # Angular 20 SPA
├── scripts/                        # Seed data, utilities
├── docker/                         # Dockerfiles per service
├── nginx/                          # Reverse proxy config
├── .github/workflows/              # CI/CD pipelines
├── docker-compose.yml              # Microservices orchestration
├── docker-compose.monolith.yml     # Monolith orchestration
├── docker-compose.prod.yml         # Production overrides
├── render.yaml                     # Render.com deployment
└── HRMSPro.sln                     # Backend solution
```

## API Endpoints

All endpoints are prefixed with `/api/{service}/`.

| Module | Base Route | Key Endpoints |
|--------|-----------|---------------|
| Auth | `/api/identity/auth` | register, login, refresh, forgot-password, verify-email, mfa |
| Employees | `/api/employee/employees` | CRUD, search, transfers, documents |
| Attendance | `/api/attendance/attendance` | clock-in/out, records, summaries |
| Leave | `/api/leave/leaveapplications` | apply, approve, reject, balances |
| Payroll | `/api/payroll/payroll` | runs, payslips, allowances, loans |
| Expense | `/api/expense/expenseclaims` | submit, approve, policies |
| Helpdesk | `/api/helpdesk/tickets` | create, assign, resolve, comments |
| Projects | `/api/projecttask/projects` | boards, sprints, tasks, bugs |
| Notifications | `/api/notifications` | send, templates, preferences, groups |

Full API docs available at `/swagger` when running in Development mode.

## Email Configuration

Set these environment variables to enable email:

```bash
Smtp__Host=smtp.gmail.com
Smtp__Port=587
Smtp__UserName=your-email@gmail.com
Smtp__Password=your-app-password
Smtp__FromEmail=your-email@gmail.com
Smtp__EnableSsl=true
```

Emails are sent for: registration verification, password reset, password changed, leave applied/approved/rejected, helpdesk ticket created/assigned/resolved.

## License

Proprietary. All rights reserved.
