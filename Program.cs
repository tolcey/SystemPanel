using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SystemPanel.Data;
using SystemPanel.Services;
using SystemPanel.Models;
using SystemPanel.Hubs;
using Serilog;
using Microsoft.OpenApi.Models;
using SystemPanel.Controllers;

AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Directory.GetCurrentDirectory(), "systempanel"));

var builder = WebApplication.CreateBuilder(args);

// Serilog Yapılandırması
builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .WriteTo.Console()
        .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day);
});

// Servisler
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

// Veritabanı Bağlantısı - Sadece bir kez eklenmeli
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servis Kayıtları
builder.Services.AddScoped<AdUserService>();
builder.Services.AddScoped<IDuyuruService, DuyuruService>();
builder.Services.AddSingleton<SccmLogService>();
builder.Services.AddSingleton<IServiceStatusService, ServiceStatusService>();
builder.Services.AddSingleton<ServerMonitoringService>();
builder.Services.AddScoped<GpoService>(); // GpoService DI konteynerine ekleniyor
builder.Services.AddScoped<FileManagementService>(); // FileManagementService servisi DI konteynerine ekleniyor
builder.Services.AddControllersWithViews(); // Genel servisler
builder.Services.AddScoped<LogService>(); // LogService DI konteynerine ekleniyor


// Identity Ayarları
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8; // Minimum şifre uzunluğu
    options.Password.RequireDigit = true; // Şifrede rakam zorunlu
    options.Password.RequireLowercase = true; // Küçük harf zorunlu
    options.Password.RequireUppercase = true; // Büyük harf zorunlu
    options.Password.RequireNonAlphanumeric = true; // Özel karakter zorunlu
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Kimlik Doğrulama Çerez Ayarları
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// Swagger Yapılandırması
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SystemPanel API",
        Version = "v1",
        Description = "SystemPanel için API dokümantasyonu",
        Contact = new OpenApiContact
        {
            Name = "Destek Ekibi",
            Email = "support@systempanel.com"
        }
    });
});

// Uygulama Başlatılıyor
var app = builder.Build();

// Veritabanı Seed İşlemi
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var configuration = services.GetRequiredService<IConfiguration>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var context = services.GetRequiredService<ApplicationDbContext>();

    await SeedDatabase(context, userManager, roleManager, configuration);
}

// Pipeline Yapılandırması
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SystemPanel API v1");
        c.RoutePrefix = "swagger";
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Rotalar
app.MapControllerRoute(
    name: "Account",
    pattern: "{controller=Account}/{action=Login}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapHub<MonitoringHub>("/monitoringHub"); // SignalR Hub

app.Run();

static async Task SeedDatabase(
    ApplicationDbContext context,
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IConfiguration configuration)
{
    try
    {
        // MapUnits tablosu için örnek veri
        if (!context.MapUnits.Any())
        {
            var mapUnits = new[] {
                new MapUnit { Name = "İstanbul", Description = "İstanbul Şehri", Latitude = 41.0082, Longitude = 28.9784 },
                new MapUnit { Name = "Ankara", Description = "Başkent Ankara", Latitude = 39.9208, Longitude = 32.8541 }
            };

            context.MapUnits.AddRange(mapUnits);
            await context.SaveChangesAsync();
        }

        // Admin kullanıcı ve rolü oluşturma
        var adminConfig = configuration.GetSection("AdminUser");
        var adminUserName = adminConfig["UserName"];
        var adminEmail = adminConfig["Email"];
        var adminPassword = adminConfig["Password"];
        var adminRole = adminConfig["Role"] ?? "Admin";

        if (!await roleManager.RoleExistsAsync(adminRole))
        {
            await roleManager.CreateAsync(new IdentityRole(adminRole));
        }

        if (await userManager.FindByNameAsync(adminUserName) == null)
        {
            var adminUser = new IdentityUser { UserName = adminUserName, Email = adminEmail, EmailConfirmed = true };
            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, adminRole);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Veritabanı oluşturulurken bir hata oluştu: {ex.Message}");
        throw;
    }
}
