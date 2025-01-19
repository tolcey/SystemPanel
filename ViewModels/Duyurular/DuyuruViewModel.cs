using Microsoft.AspNetCore.Mvc;

namespace SystemPanel.ViewModels.Duyurular
{
    public class DuyuruViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
    }

}
