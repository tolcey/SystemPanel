using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemPanel.Models;
using SystemPanel.Services;

public class ServiceStatusService : IServiceStatusService
{
    private readonly List<ServiceStatus> _services = new()
    {
        new ServiceStatus { Id = 1, ServiceName = "CcmExec", State = "Running", Status = "OK" },
        new ServiceStatus { Id = 2, ServiceName = "WDSServer", State = "Stopped", Status = "Warning" }
    };

    public async Task<IEnumerable<ServiceStatus>> GetAllServiceStatusesAsync()
    {
        return await Task.FromResult(_services);
    }

    public async Task StartServiceAsync(int id)
    {
        var service = _services.FirstOrDefault(s => s.Id == id);
        if (service != null)
        {
            service.State = "Running";
            service.Status = "OK";
        }
        await Task.CompletedTask;
    }

    public async Task StopServiceAsync(int id)
    {
        var service = _services.FirstOrDefault(s => s.Id == id);
        if (service != null)
        {
            service.State = "Stopped";
            service.Status = "Warning";
        }
        await Task.CompletedTask;
    }
}
