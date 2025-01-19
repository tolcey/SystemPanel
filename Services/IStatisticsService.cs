using System.Threading.Tasks;
using SystemPanel.Models;

namespace SystemPanel.Services;

public interface IStatisticsService
{
    Task<StatisticsViewModel> GetStatisticsAsync();
}
