#!/bin/bash
# Docker Build ve Run Script (Mac/Linux)
# Bu script projeyi publish edip Docker container'larını çalıştırır

echo "=== PayrollEngine Docker Build Script ==="

# 1. Temizlik
echo -e "\n[1/4] Docker container'ları durduruluyor..."
docker-compose down

# 2. API Publish
echo -e "\n[2/4] API projesi publish ediliyor..."
dotnet publish PayrollEngine.Web.API/PayrollEngine.Web.API.csproj -c Release
if [ $? -ne 0 ]; then
    echo "❌ API publish başarısız!"
    exit 1
fi

# 3. UI Publish
echo -e "\n[3/4] UI projesi publish ediliyor..."
dotnet publish PayrollEngine.Web.UI/PayrollEngine.Web.UI.csproj -c Release
if [ $? -ne 0 ]; then
    echo "❌ UI publish başarısız!"
    exit 1
fi

# 4. Docker Build ve Run
echo -e "\n[4/4] Docker image'ları oluşturuluyor ve container'lar başlatılıyor..."
docker-compose up -d --build

if [ $? -eq 0 ]; then
    echo -e "\n✓ Başarılı! Uygulamanız çalışıyor."
    echo "  - UI:  http://localhost:5033"
    echo "  - API: http://localhost:5032"
    echo -e "\nLogları görmek için: docker-compose logs -f"
    echo "Durdurmak için:       docker-compose down"
else
    echo -e "\n✗ Docker container başlatılamadı!"
    echo "Logları kontrol edin: docker-compose logs"
fi
