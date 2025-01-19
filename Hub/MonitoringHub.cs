using Microsoft.AspNetCore.SignalR;
using SystemPanel.Models;

namespace SystemPanel.Hubs
{
    public class MonitoringHub : Hub
    {
        public async Task SendMetrics(ServerMetrics metrics)
        {
            await Clients.All.SendAsync("UpdateMetrics", metrics);
        }
    }
}
