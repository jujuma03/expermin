using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.ENTITIES.Models
{
    public class Collaborator : BaseEntity
    {
        public string Name { get; set; }
        public Guid MediaFileId { get; set; }
        public MediaFile MediaFile { get; set; }
        public byte? Order { get; set; } = 255;
        public byte Status { get; set; } // active = 1, hidden = 2
        public DateTime PublicationDate { get; set; }
    }
}
