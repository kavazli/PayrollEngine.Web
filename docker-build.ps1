# Docker Build and Run Script
# This script publishes the project and runs Docker containers

Write-Host "=== PayrollEngine Docker Build Script ===" -ForegroundColor Cyan

# 1. Clean up
Write-Host "`n[1/4] Stopping Docker containers..." -ForegroundColor Yellow
docker-compose down

# 2. Publish API
Write-Host "`n[2/4] Publishing API project..." -ForegroundColor Yellow
dotnet publish PayrollEngine.Web.API/PayrollEngine.Web.API.csproj -c Release
if ($LASTEXITCODE -ne 0) {
    Write-Host "API publish failed!" -ForegroundColor Red
    exit 1
}

# 3. Publish UI
Write-Host "`n[3/4] Publishing UI project..." -ForegroundColor Yellow
dotnet publish PayrollEngine.Web.UI/PayrollEngine.Web.UI.csproj -c Release
if ($LASTEXITCODE -ne 0) {
    Write-Host "UI publish failed!" -ForegroundColor Red
    exit 1
}

# 4. Docker Build and Run
Write-Host "`n[4/4] Building Docker images and starting containers..." -ForegroundColor Yellow
docker-compose up -d --build

if ($LASTEXITCODE -eq 0) {
    Write-Host "`nSuccess! Your application is running." -ForegroundColor Green
    Write-Host "  - UI:  http://localhost:5033" -ForegroundColor Cyan
    Write-Host "  - API: http://localhost:5032" -ForegroundColor Cyan
    Write-Host "`nTo view logs: docker-compose logs -f" -ForegroundColor Gray
    Write-Host "To stop:      docker-compose down" -ForegroundColor Gray
} else {
    Write-Host "`nDocker container failed to start!" -ForegroundColor Red
    Write-Host "Check logs: docker-compose logs" -ForegroundColor Yellow
}
