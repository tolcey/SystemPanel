using System.ComponentModel.DataAnnotations;

namespace SystemPanel.ViewModels.AdUser
{
    public class EditAdUserViewModel
    {
        public string UserName { get; set; }

        [StringLength(100, ErrorMessage = "Görünen ad 100 karakteri geçemez.")]
        public string DisplayName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
