using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SystemPanel.Models;

namespace SystemPanel.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tablolar
        public DbSet<ArizaTalep> ArizaTalepler { get; set; }
        public DbSet<Birim> Birimler { get; set; }
        public DbSet<BrmTlpTurleri> BrmTlpTurleri { get; set; }
        public DbSet<Duyuru> Duyurular { get; set; }
        public DbSet<HataLog> HataLoglar { get; set; }
        public DbSet<HavaleYetkisi> HavaleYetkileri { get; set; }
        public DbSet<Havale> Havaleler { get; set; }
        public DbSet<IslemLog> IslemLoglar { get; set; }
        public DbSet<SccmLog> SccmLogs { get; set; } // EKLENDİ
        public DbSet<Sms> Smsler { get; set; }
        public DbSet<SmsGidenler> SmsGidenler { get; set; }
        public DbSet<TalepCevap> TalepCevaplar { get; set; }
        public DbSet<Test> Testler { get; set; }
        public DbSet<TreeIl> TreeIller { get; set; }
        public DbSet<Yetki> Yetkiler { get; set; }
        public DbSet<MapUnit> MapUnits { get; set; }
        public DbSet<Module> Modules { get; set; }

        // LogEntries DbSet'i eklendi
        public DbSet<LogEntry> LogEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Identity tabloları için birincil anahtar ayarları
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            // Diğer tablolar arası ilişkiler
            modelBuilder.Entity<ArizaTalep>().HasKey(a => a.Id);
            modelBuilder.Entity<Birim>().HasKey(b => b.BirimId);
            modelBuilder.Entity<BrmTlpTurleri>().HasKey(b => b.TLP_tur_id);
            modelBuilder.Entity<Havale>().HasOne<ArizaTalep>().WithMany().HasForeignKey(h => h.TalepId);
            modelBuilder.Entity<TalepCevap>().HasOne<ArizaTalep>().WithMany().HasForeignKey(tc => tc.TalepId);
            modelBuilder.Entity<IslemLog>().HasKey(il => il.LogId);
            modelBuilder.Entity<HataLog>().HasKey(hl => hl.LogId);
            modelBuilder.Entity<Sms>().HasKey(s => s.Id);
            modelBuilder.Entity<SmsGidenler>().HasKey(sg => sg.Id);
            modelBuilder.Entity<Yetki>().HasKey(y => y.Id);
            modelBuilder.Entity<TreeIl>().HasKey(ti => ti.SayfaId);
        }
    }
}
