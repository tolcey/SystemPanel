public class GpoService
{
    private readonly string _domain;
    private readonly string _username;
    private readonly string _password;
    private readonly ILogger<GpoService> _logger;

    public GpoService(IConfiguration configuration, ILogger<GpoService> logger)
    {
        var adConfig = configuration.GetSection("ActiveDirectory");
        _domain = adConfig["Domain"];
        _username = adConfig["Username"];
        _password = adConfig["Password"];
        _logger = logger;
    }

    public List<string> GetAllGpos()
    {
        try
        {
            // GPO listeleme mantığı burada uygulanacak
            return new List<string> { "GPO1", "GPO2", "GPO3" }; // Örnek
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching GPO list.");
            return new List<string>();
        }
    }

    public bool CreateGpo(string name, string description)
    {
        try
        {
            // GPO oluşturma mantığı
            _logger.LogInformation("GPO {Name} created with description {Description}.", name, description);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating GPO {Name}.", name);
            return false;
        }
    }

    public bool DeleteGpo(string gpoId)
    {
        try
        {
            // GPO silme mantığı
            _logger.LogInformation("GPO {GpoId} deleted successfully.", gpoId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting GPO {GpoId}.", gpoId);
            return false;
        }
    }
}
