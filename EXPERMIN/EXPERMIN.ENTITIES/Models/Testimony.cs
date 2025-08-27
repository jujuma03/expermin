using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.ENTITIES.Models
{
    public class Testimony : BaseEntity
    {
        public string ClientName { get; set; } = null!;
        public Guid MediaFileId { get; set; }
        public MediaFile MediaFile { get; set; }
        public byte Rating { get; set; } // 1 to 5
        public string Comment { get; set; } = null!;
        public byte? Order { get; set; } = 255;
        public byte Status { get; set; } // active = 1, hidden = 2
        public DateTime PublicationDate { get; set; }
    }
}
