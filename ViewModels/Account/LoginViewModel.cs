using System.ComponentModel.DataAnnotations;

namespace SystemPanel.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        // Kullanıcı türünü ayırmak için eklenen özellik
        public bool IsAdUser { get; set; }
    }
}
