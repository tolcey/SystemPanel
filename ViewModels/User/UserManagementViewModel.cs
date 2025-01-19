using Microsoft.AspNetCore.Identity;

namespace SystemPanel.ViewModels.User
{
    public class UserManagementViewModel
    {
        // Identity kullanıcı bilgileri
        public List<IdentityUser> IdentityUsers { get; set; }

        // Active Directory kullanıcı bilgileri
        public List<SystemPanel.Models.AdUser> AdUsers { get; set; }

        // Diğer kullanıcı bilgileri eklenebilir
    }
}
