using System.ComponentModel.DataAnnotations;

namespace SystemPanel.Models
{
    public class AdUser
    {
        [Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Görünen ad gereklidir.")]
        [StringLength(100, ErrorMessage = "Görünen ad 100 karakteri geçemez.")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Email adresi gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi girin.")]
        public string Email { get; set; }
    }
}
