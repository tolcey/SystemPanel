from pathlib import Path

# Ana README.md içeriği
readme_content = """
# 🖥️ System Admin Lab Serisi

Bu proje, sistem yöneticiliğine giriş yapmak isteyenler için 7 günlük uygulamalı bir lab ortamı sunar. Windows Server, Active Directory, GPO, DNS, DHCP, PowerShell gibi konulara odaklanır.

---

## 🚀 Hedef

- Sistem yönetimi konularında pratik bilgi kazanmak
- AD ve GPO gibi kurumsal altyapı bileşenlerini deneyimlemek
- PowerShell ile otomasyon ve günlük görevleri uygulamak

---

## 📂 Klasör Yapısı

| Klasör | Açıklama |
|--------|----------|
| `Day01_AD_Setup` | Active Directory kurulumu |
| `Day02_GPO_Practice` | GPO örnekleri ve politikalar |
| `Day03_DHCP_DNS_Join` | DHCP & DNS yapılandırması |
| `Day04_PowerShell_Basics` | PowerShell temel komutlar |
| `Day05_Security_Troubleshooting` | Event Viewer ve sorun giderme |
| `Scripts` | CSV ile kullanıcı oluşturma ve export scriptleri |

---

## 🛠️ Lab Yapılandırması

| Rol | Bilgisayar Adı | Açıklama |
|-----|----------------|----------|
| Domain Controller | `DC01` | Windows Server 2022 – AD, DNS, DHCP |
| İstemci | `CL01` | Windows 10 – domain’e katılıp test edilir |

---

## 📚 Kullanılan Teknolojiler

- Windows Server 2022
- Active Directory Domain Services
- Group Policy Management
- PowerShell
- DHCP / DNS
- Event Viewer

---

## 📌 Başlangıç Önerileri

1. Hyper-V veya VirtualBox ile lab ortamını kur
2. `Day01_AD_Setup` klasörüyle başlamanı öneririz
3. Her günü tamamladıkça GitHub üzerinden belgelemeyi unutma

---

## 🔗 Faydalı Kaynaklar

- [Microsoft Learn](https://learn.microsoft.com/)
- [tolgaceyhan.com](https://tolgaceyhan.com) – Blog yazıları
- [GitHub Profili](https://github.com/USERNAME)

---

## ✍️ Katkı

Kendi lab örneklerinizi paylaşmak isterseniz fork'layıp pull request gönderebilirsiniz.

---

## 📂 Dosya: `README.md`
"""

# Dosyayı oluştur
readme_path = "/mnt/data/README.md"
Path(readme_path).write_text(readme_content)

readme_path
