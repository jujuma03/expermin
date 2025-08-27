using EXPERMIN.WEB.Models.Portal.MediaFile;

namespace EXPERMIN.WEB.Models.Portal.Collaborator
{
    public class CollaboratorViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public MediaFileViewModel Image { get; set; }
    }
}
