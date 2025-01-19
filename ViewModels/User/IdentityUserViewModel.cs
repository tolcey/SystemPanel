using System.ComponentModel.DataAnnotations;

namespace SystemPanel.ViewModels.User
{
    public class IdentityUserViewModel
    {
        public string Id { get; set; }  // Kullanıcı ID'si (gerekli)
        [Required]
        public string UserName { get; set; }  // Kullanıcı adı
        [Required]
        [EmailAddress]
        public string Email { get; set; }  // Email adresi
        [Required]
        public string Password { get; set; }  // Şifre

        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }  // Şifre tekrar doğrulaması
    }
}
