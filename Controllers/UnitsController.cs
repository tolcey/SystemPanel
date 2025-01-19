using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemPanel.Models;
using SystemPanel.Services;

namespace SystemPanel.Controllers;

public class UnitsController : Controller
{
    private readonly IUnitService _service;

    public UnitsController(IUnitService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Computers()
    {
        var computers = await _service.GetUnitComputersAsync();
        return View(computers);
    }
}
