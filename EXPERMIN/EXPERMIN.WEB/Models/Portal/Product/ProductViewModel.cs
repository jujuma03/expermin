using EXPERMIN.WEB.Models.Portal.MediaFile;

namespace EXPERMIN.WEB.Models.Portal.Product
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public MediaFileViewModel Image { get; set; }
    }
}
