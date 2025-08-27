using EXPERMIN.WEB.Models.Portal.MediaFile;

namespace EXPERMIN.WEB.Models.Portal.Testimony
{
    public class TestimonyViewModel
    {
        public string Id { get; set; }
        public string ClientName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int Order { get; set; }
        public MediaFileViewModel Image { get; set; }
    }
}
