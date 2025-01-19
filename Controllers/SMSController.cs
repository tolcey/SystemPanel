using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SystemPanel.Services;
using SystemPanel.Models;

namespace SystemPanel.Controllers
{
    public class SMSController : Controller
    {
        private readonly ISMSService _service;

        public SMSController(ISMSService service)
        {
            _service = service;
        }

        public IActionResult Send() => View();

        [HttpPost]
        public async Task<IActionResult> Send(SMSRequest model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.SendSMSAsync(model);
                    ViewBag.Message = "SMS sent successfully!";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error sending SMS: {ex.Message}");
                }
            }
            return View(model);
        }
    }
}
