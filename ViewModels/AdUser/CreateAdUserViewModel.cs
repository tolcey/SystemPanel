using System.ComponentModel.DataAnnotations;

namespace SystemPanel.ViewModels.AdUser
{
    public class CreateAdUserViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Görünen ad 100 karakteri geçemez.")]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
