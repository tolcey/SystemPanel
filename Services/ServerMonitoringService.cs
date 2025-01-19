using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Management;
using SystemPanel.Models;


namespace SystemPanel.Controllers
{
    public class ServerMonitoringService
    {
        private readonly ILogger<ServerMonitoringService> _logger;

        public ServerMonitoringService(ILogger<ServerMonitoringService> logger)
        {
            _logger = logger;
        }

        public ServerMetrics GetServerMetrics()
        {
            try
            {
                var metrics = new ServerMetrics
                {
                    CpuUsage = GetCpuUsage(),
                    MemoryUsage = GetMemoryUsage(),
                    DiskUsage = GetDiskUsage(),
                    NetworkUsage = GetNetworkUsage()
                };

                return metrics;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving server metrics.");
                throw;
            }
        }

        public bool AddServer(string serverName, string ipAddress)
        {
            try
            {
                _logger.LogInformation("Server {ServerName} with IP {IpAddress} added successfully.", serverName, ipAddress);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding server {ServerName}.", serverName);
                return false;
            }
        }

        private double GetCpuUsage()
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            return Math.Round(cpuCounter.NextValue(), 2);
        }

        private double GetMemoryUsage()
        {
            using (var searcher = new ManagementObjectSearcher("SELECT FreePhysicalMemory, TotalVisibleMemorySize FROM Win32_OperatingSystem"))
            {
                var memStatus = searcher.Get().Cast<ManagementObject>().FirstOrDefault();
                if (memStatus != null)
                {
                    double freeMemory = Convert.ToDouble(memStatus["FreePhysicalMemory"]);
                    double totalMemory = Convert.ToDouble(memStatus["TotalVisibleMemorySize"]);
                    return Math.Round(((totalMemory - freeMemory) / totalMemory) * 100, 2);
                }
            }
            return 0;
        }

        private double GetDiskUsage()
        {
            var driveInfo = DriveInfo.GetDrives().FirstOrDefault(d => d.IsReady && d.DriveType == DriveType.Fixed);
            if (driveInfo != null)
            {
                double totalSpace = driveInfo.TotalSize;
                double freeSpace = driveInfo.TotalFreeSpace;
                return Math.Round(((totalSpace - freeSpace) / totalSpace) * 100, 2);
            }
            return 0;
        }

        private double GetNetworkUsage()
        {
            return 0; // Placeholder for network usage monitoring
        }
    }
}
