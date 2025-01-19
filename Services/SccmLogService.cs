using System.Management;
using SystemPanel.Models;

public class SccmLogService
{
    private readonly string _sccmServer;
    private readonly string _username;
    private readonly string _password;

    public SccmLogService(IConfiguration configuration)
    {
        _sccmServer = configuration["SCCM:Server"];
        _username = configuration["SCCM:Username"];
        _password = configuration["SCCM:Password"];
    }

    public List<DpServerInfo> FetchDistributionPointsAndServices()
    {
        var dpServers = new List<DpServerInfo>();
        try
        {
            var scope = new ManagementScope($"\\\\{_sccmServer}\\root\\SMS\\site_XXX", new ConnectionOptions
            {
                Username = _username,
                Password = _password
            });
            scope.Connect();

            var query = new ObjectQuery("SELECT * FROM SMS_DistributionPoint");
            var searcher = new ManagementObjectSearcher(scope, query);

            foreach (var dp in searcher.Get())
            {
                dpServers.Add(new DpServerInfo
                {
                    ServerName = dp["ServerName"]?.ToString(),
                    IpAddress = "Unknown", // Örnek, IP bilgisi burada çekilebilir
                    Services = new List<ServiceStatus>
                    {
                        new ServiceStatus { ServiceName = "CcmExec", State = "Running", Status = "OK" }
                    }
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching DP servers: {ex.Message}");
        }
        return dpServers;
    }
}
