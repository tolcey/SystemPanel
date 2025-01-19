
using System.Threading.Tasks;
using SystemPanel.Models;

namespace SystemPanel.Services;

public interface ISMSService
{
    Task SendSMSAsync(SMSRequest request);
}
