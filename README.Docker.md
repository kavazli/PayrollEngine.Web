# Docker ile Çalıştırma Kılavuzu

Bu proje Docker ile kolayca çalıştırılabilir. Artık VS Code'da manuel olarak API ve UI'ı ayrı ayrı çalıştırmanıza gerek yok!

## Gereksinimler

- Docker Desktop (Windows için)
- Docker Compose (genellikle Docker Desktop ile gelir)
- .NET 9.0 SDK (local build için)

## Kurulum ve Çalıştırma

### İlk Kurulum

1. Docker Desktop'ın çalıştığından emin olun

2. **Otomatik Build (Önerilen):**
   Proje ana dizininde terminalde şu komutu çalıştırın:
   ```powershell
   .\docker-build.ps1
   ```
   
   Bu script:
   - Önce projeleri local'de publish eder (.NET bağımlılık sorunlarını çözer)
   - Docker image'larını oluşturur
   - Container'ları başlatır

3. **Manuel Build:**
   ```powershell
   # Projeleri publish et
   dotnet publish PayrollEngine.Web.API/PayrollEngine.Web.API.csproj -c Release
   dotnet publish PayrollEngine.Web.UI/PayrollEngine.Web.UI.csproj -c Release
   
   # Docker container'ları başlat
   docker-compose up -d --build
   ```

Erişim adresleri:
- API: http://localhost:5032
- UI: http://localhost:5033

### Günlük Kullanım

**En Kolay Yol (Build Script):**
```powershell
.\docker-build.ps1
```

**Servisleri Başlat (build olmadan):**
```powershell
docker-compose up -d
```

**Kod Değişikliklerinden Sonra:**
```powershell
# Otomatik publish + build
.\docker-build.ps1
./docker-build.sh # mac için

# VEYA manuel
dotnet publish PayrollEngine.Web.API/PayrollEngine.Web.API.csproj -c Release
dotnet publish PayrollEngine.Web.UI/PayrollEngine.Web.UI.csproj -c Release
docker-compose up -d --build
```

**Servisleri Durdur:**
```powershell
docker-compose down
```

**Logları İzle:**
```powershell
# Tüm servislerin logları
docker-compose logs -f

# Sadece API logları
docker-compose logs -f api

# Sadece UI logları
docker-compose logs -f ui
```

**Servis Durumunu Kontrol Et:**
```powershell
docker-compose ps
```

**Servisleri Yeniden Başlat:**
```powershell
docker-compose restart
```

**Kod Değişikliklerinden Sonra Yeniden Build:**
```powershell
.\docker-build.ps1
```

## Erişim Adresleri

- **UI (Blazor WebAssembly):** http://localhost:5033
- **API (Web API):** http://localhost:5032
- **API Documentation (Scalar):** http://localhost:5032/scalar/v1

## Veritabanı

SQLite veritabanı `./data/payroll.db` konumunda host makinenizde saklanır. Container silinse bile verileriniz kaybolmaz.

## Sorun Giderme

**Port zaten kullanılıyor hatası:**
```powershell
# Çalışan servisleri kontrol et
docker-compose ps

# Servisleri durdur
docker-compose down
```

**Image güncellemesi gerekiyor:**
```powershell
# Tüm container ve image'ları temizle ve yeniden oluştur
docker-compose down
docker-compose up -d --build --force-recreate
```

**Container loglarını görmek:**
```powershell
docker-compose logs api
docker-compose logs ui
```

**Tüm Docker kaynaklarını temizle (DİKKAT: Tüm Docker verileri silinir!):**
```powershell
docker system prune -a --volumes
```

## Geliştirme Notları

- **Önemli**: Kod değişikliğinden sonra projeleri local'de publish etmeniz ve Docker'ı yeniden build etmeniz gerekir
- Build script (`docker-build.ps1`) bunu otomatik yapar
- UI, API'ı http://localhost:5032 üzerinden çağırır
- Her iki servis de aynı Docker network üzerinde çalışır
- CORS ayarları tüm origin'lere izin verir (production'da düzenlenmeli)
- SQLite dosyası volume olarak bağlıdır, veriler kalıcıdır
- Docker image'lar publish edilmiş dosyalardan oluşturulur (NuGet SSL sorununu çözmek için)
