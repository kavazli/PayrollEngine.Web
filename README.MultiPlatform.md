# Multi-Platform Kullanım (Windows + Mac)

Bu proje hem Windows hem Mac'te Docker ile çalışır.

## GitHub Workflow

### İlk Kurulum (Her Bilgisayarda Bir Kez)

**Windows:**
```powershell
git clone https://github.com/kullanici-adi/PayrollEngine.Web.git
cd PayrollEngine.Web
.\docker-build.ps1
```

**Mac:**
```bash
git clone https://github.com/kullanici-adi/PayrollEngine.Web.git
cd PayrollEngine.Web
chmod +x docker-build.sh
./docker-build.sh
```

## Günlük Kullanım

### Windows'ta:
```powershell
# Güncellemeleri çek
git pull

# Kod değiştirdiysen
.\docker-build.ps1

# GitHub'a gönder
git add .
git commit -m "açıklama"
git push
```

### Mac'te:
```bash
# Güncellemeleri çek
git pull

# Kod değiştirdiysen
./docker-build.sh

# GitHub'a gönder
git add .
git commit -m "açıklama"
git push
```

## Önemli Notlar

### Dosyalar Git'e Gitmemeli:
- ✅ `.gitignore` zaten ayarlandı:
  - `bin/`, `obj/` (build çıktıları)
  - `data/` (veritabanı)
  - `*.db` (SQLite dosyaları)
  - `.vs/`, `.vscode/` (IDE ayarları)

### Git'e Gitmesi Gerekenler:
- ✅ `docker-build.ps1` (Windows script)
- ✅ `docker-build.sh` (Mac script)
- ✅ `docker-compose.yml`
- ✅ `Dockerfile`'lar
- ✅ Kaynak kod dosyaları (.cs, .razor, .json)

### Senkronizasyon İpuçları:

1. **Her gün işe başlarken:** `git pull`
2. **Kod değiştirdikten sonra:** Build script çalıştır
3. **Gün sonunda:** `git push`
4. **Conflict olursa:** Merge editor kullan veya yardım iste

### Veritabanı Senkronizasyonu:

⚠️ **DİKKAT:** SQLite veritabanı (`data/payroll.db`) Git'e gitmez!

**Seçenekler:**
- **A) Her ortamda ayrı test verisi** (önerilen geliştirme için)
- **B) Veritabanını manuel kopyala** (OneDrive, USB vs.)
- **C) Seeder kullan** (`PayrollEngine.Web.Seeder` projeniz var)

## Docker Desktop Gereksinimleri

**Windows:**
- Docker Desktop for Windows
- WSL 2 backend aktif

**Mac:**
- Docker Desktop for Mac (Apple Silicon veya Intel)
- Rosetta 2 (M1/M2 için .NET 9.0)

## Sorun Giderme

**Port çakışması:**
```bash
# Windows
netstat -ano | findstr :5032

# Mac
lsof -i :5032
```

**Container çalışmıyor:**
```bash
docker-compose logs -f
docker-compose ps
```

**Build hatası:**
```bash
# Temiz başlat
docker-compose down
docker system prune -f
# Tekrar build
```
