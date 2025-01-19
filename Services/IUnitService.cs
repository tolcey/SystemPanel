using System.Collections.Generic;
using System.Threading.Tasks;
using SystemPanel.Models;

namespace SystemPanel.Services;

public interface IUnitService
{
    Task<IEnumerable<UnitAdmin>> GetUnitAdminsAsync();
    Task<IEnumerable<UnitComputer>> GetUnitComputersAsync();
}
