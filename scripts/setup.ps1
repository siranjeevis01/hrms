<#
.SYNOPSIS
    HRMS Pro Docker Setup Script
.DESCRIPTION
    This script sets up the HRMS Pro development environment using Docker Compose.
    It checks for Docker, copies environment file, starts services, and verifies health.
.EXAMPLE
    .\scripts\setup.ps1
.EXAMPLE
    .\scripts\setup.ps1 -SkipHealthCheck
.EXAMPLE
    .\scripts\setup.ps1 -Environment Production
#>

param(
    [switch]$SkipHealthCheck,
    [switch]$Force,
    [string]$Environment = "Development",
    [int]$HealthCheckTimeout = 300
)

$ErrorActionPreference = "Stop"
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$RootDir = Split-Path -Parent $ScriptDir

# ============================================
# Helper Functions
# ============================================

function Write-Header {
    param([string]$Message)
    Write-Host ""
    Write-Host "============================================" -ForegroundColor Cyan
    Write-Host "  $Message" -ForegroundColor Cyan
    Write-Host "============================================" -ForegroundColor Cyan
    Write-Host ""
}

function Write-Step {
    param([string]$Message)
    Write-Host "[STEP] $Message" -ForegroundColor Yellow
}

function Write-Success {
    param([string]$Message)
    Write-Host "[OK] $Message" -ForegroundColor Green
}

function Write-Error {
    param([string]$Message)
    Write-Host "[ERROR] $Message" -ForegroundColor Red
}

function Write-Info {
    param([string]$Message)
    Write-Host "[INFO] $Message" -ForegroundColor White
}

function Test-DockerRunning {
    try {
        $dockerVersion = docker --version 2>&1
        if ($LASTEXITCODE -ne 0) {
            return $false
        }
        $composeVersion = docker-compose --version 2>&1
        if ($LASTEXITCODE -ne 0) {
            # Try docker compose (v2 plugin)
            $composeVersion = docker compose version 2>&1
            if ($LASTEXITCODE -ne 0) {
                return $false
            }
        }
        return $true
    }
    catch {
        return $false
    }
}

function Get-DockerComposeCommand {
    # Check for docker compose v2 first
    try {
        $result = docker compose version 2>&1
        if ($LASTEXITCODE -eq 0) {
            return "docker compose"
        }
    }
    catch {}
    
    # Fall back to docker-compose
    try {
        $result = docker-compose --version 2>&1
        if ($LASTEXITCODE -eq 0) {
            return "docker-compose"
        }
    }
    catch {}
    
    return $null
}

function Wait-ForServiceHealth {
    param(
        [string]$ServiceName,
        [string]$ContainerName,
        [int]$TimeoutSeconds = 60
    )
    
    $elapsed = 0
    $interval = 5
    
    Write-Info "Waiting for $ServiceName to be healthy..."
    
    while ($elapsed -lt $TimeoutSeconds) {
        try {
            $health = docker inspect --format='{{.State.Health.Status}}' $ContainerName 2>&1
            if ($health -eq "healthy") {
                Write-Success "$ServiceName is healthy"
                return $true
            }
            elseif ($health -eq "unhealthy") {
                Write-Error "$ServiceName is unhealthy"
                return $false
            }
        }
        catch {
            # Container might not be running yet
        }
        
        Start-Sleep -Seconds $interval
        $elapsed += $interval
        Write-Host "." -NoNewline
    }
    
    Write-Host ""
    Write-Error "$ServiceName health check timed out after $TimeoutSeconds seconds"
    return $false
}

# ============================================
# Main Script
# ============================================

Write-Header "HRMS Pro - Docker Setup Script"
Write-Info "Environment: $Environment"
Write-Info "Working Directory: $RootDir"

# Step 1: Check Docker
Write-Step "Checking Docker installation..."
if (-not (Test-DockerRunning)) {
    Write-Error "Docker is not running or not installed."
    Write-Info "Please install Docker Desktop from https://docs.docker.com/desktop/install/windows-install/"
    Write-Info "After installation, start Docker Desktop and try again."
    exit 1
}
Write-Success "Docker is installed and running"

# Get docker compose command
$ComposeCmd = Get-DockerComposeCommand
if (-not $ComposeCmd) {
    Write-Error "Docker Compose is not available."
    Write-Info "Please install Docker Compose or update Docker Desktop."
    exit 1
}
Write-Success "Docker Compose is available: $ComposeCmd"

# Step 2: Create required directories
Write-Step "Creating required directories..."
$directories = @(
    "$RootDir\logs\api-gateway",
    "$RootDir\logs\identity-service",
    "$RootDir\logs\organization-service",
    "$RootDir\logs\employee-service",
    "$RootDir\logs\attendance-service",
    "$RootDir\logs\leave-service",
    "$RootDir\logs\payroll-service",
    "$RootDir\logs\notification-service",
    "$RootDir\logs\recruitment-service",
    "$RootDir\docker\mysql\conf.d"
)

foreach ($dir in $directories) {
    if (-not (Test-Path $dir)) {
        New-Item -ItemType Directory -Path $dir -Force | Out-Null
    }
}
Write-Success "Directories created"

# Step 3: Copy .env.example to .env
Write-Step "Setting up environment file..."
$envFile = Join-Path $RootDir ".env"
$envExampleFile = Join-Path $RootDir ".env.example"

if (-not (Test-Path $envExampleFile)) {
    Write-Error ".env.example file not found at $envExampleFile"
    exit 1
}

if ((Test-Path $envFile) -and -not $Force) {
    Write-Info ".env file already exists. Skipping copy (use -Force to overwrite)"
}
else {
    Copy-Item -Path $envExampleFile -Destination $envFile -Force
    Write-Success "Copied .env.example to .env"
    Write-Info "Please edit .env file with your configuration"
}

# Step 4: Build and start containers
Write-Step "Building and starting Docker containers..."

$composeFiles = @("-f", "docker-compose.yml")

if ($Environment -eq "Production") {
    $composeFiles += @("-f", "docker-compose.prod.yml")
    Write-Info "Using production configuration"
}
else {
    $composeFiles += @("-f", "docker-compose.override.yml")
    Write-Info "Using development configuration"
}

# Pull latest images
Write-Step "Pulling latest images..."
$pullCmd = "$ComposeCmd $($composeFiles -join ' ') pull --ignore-pull-failures"
Invoke-Expression $pullCmd

# Build and start
Write-Step "Starting services..."
$upCmd = "$ComposeCmd $($composeFiles -join ' ') up -d --build"
Invoke-Expression $upCmd

if ($LASTEXITCODE -ne 0) {
    Write-Error "Failed to start Docker containers"
    exit 1
}

Write-Success "Docker containers started"

# Step 5: Wait for health checks
if (-not $SkipHealthCheck) {
    Write-Step "Waiting for services to be healthy..."
    
    $services = @(
        @{Name="MySQL"; Container="hrms-mysql"; Timeout=120},
        @{Name="Redis"; Container="hrms-redis"; Timeout=60},
        @{Name="RabbitMQ"; Container="hrms-rabbitmq"; Timeout=90},
        @{Name="Elasticsearch"; Container="hrms-elasticsearch"; Timeout=180},
        @{Name="Jaeger"; Container="hrms-jaeger"; Timeout=60},
        @{Name="API Gateway"; Container="hrms-api-gateway"; Timeout=120},
        @{Name="Identity Service"; Container="hrms-identity-service"; Timeout=120},
        @{Name="Frontend"; Container="hrms-frontend"; Timeout=120}
    )
    
    $allHealthy = $true
    foreach ($service in $services) {
        if (-not (Wait-ForServiceHealth -ServiceName $service.Name -ContainerName $service.Container -TimeoutSeconds $service.Timeout)) {
            $allHealthy = $false
            Write-Warning "Service $($service.Name) is not healthy"
        }
        Write-Host ""
    }
    
    if ($allHealthy) {
        Write-Success "All services are healthy!"
    }
    else {
        Write-Warning "Some services may not be healthy. Check logs for details."
    }
}

# Step 6: Print status
Write-Header "Service Status"

$psCmd = "$ComposeCmd $($composeFiles -join ' ') ps"
Invoke-Expression $psCmd

# Step 7: Print access information
Write-Header "Access Information"

Write-Host "Frontend:        http://localhost:4200" -ForegroundColor Green
Write-Host "API Gateway:     http://localhost:5000" -ForegroundColor Green
Write-Host "MySQL:           localhost:3306" -ForegroundColor Green
Write-Host "Redis:           localhost:6379" -ForegroundColor Green
Write-Host "RabbitMQ:        http://localhost:15672" -ForegroundColor Green
Write-Host "Elasticsearch:   http://localhost:9200" -ForegroundColor Green
Write-Host "Kibana:          http://localhost:5601" -ForegroundColor Green
Write-Host "Jaeger:          http://localhost:16686" -ForegroundColor Green
Write-Host ""
Write-Host "Service Ports:" -ForegroundColor Yellow
Write-Host "  Identity:      http://localhost:5001" -ForegroundColor White
Write-Host "  Organization:  http://localhost:5002" -ForegroundColor White
Write-Host "  Employee:      http://localhost:5003" -ForegroundColor White
Write-Host "  Attendance:    http://localhost:5004" -ForegroundColor White
Write-Host "  Leave:         http://localhost:5005" -ForegroundColor White
Write-Host "  Payroll:       http://localhost:5006" -ForegroundColor White
Write-Host "  Notification:  http://localhost:5007" -ForegroundColor White
Write-Host "  Recruitment:   http://localhost:5008" -ForegroundColor White
Write-Host ""

# Step 8: Useful commands
Write-Header "Useful Commands"

Write-Host "View logs:       $ComposeCmd logs -f [service-name]" -ForegroundColor White
Write-Host "Stop services:   $ComposeCmd down" -ForegroundColor White
Write-Host "Restart:         $ComposeCmd restart [service-name]" -ForegroundColor White
Write-Host "Rebuild:         $ComposeCmd up -d --build [service-name]" -ForegroundColor White
Write-Host "View status:     $ComposeCmd ps" -ForegroundColor White
Write-Host "Execute command: $ComposeCmd exec [service-name] [command]" -ForegroundColor White
Write-Host ""

Write-Header "Setup Complete"
Write-Success "HRMS Pro development environment is ready!"
Write-Info "Run '.\scripts\seed-data.ps1' to seed initial data"