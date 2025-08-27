using EXPERMIN.WEB.Models.Portal.MediaFile;

namespace EXPERMIN.WEB.Models.Portal.Banner
{
    public class BannerViewModel
    {
        public string Id { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public bool RouteType { get; set; }
        public int Order { get; set; }
        public string UrlDirection { get; set; }
        public bool StatusDirection { get; set; }
        public string NameDirection { get; set; }
        public MediaFileViewModel Image { get; set; }
    }
}
