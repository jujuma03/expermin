using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.ENTITIES.Models
{
    public class Banner : BaseEntity
    {
        public string Headline { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; } //se actualiza cuando lo pones a estado "Activo"
        public byte Status { get; set; } // activo = 1, oculto = 2
        public byte RouteType { get; set; }
        public Guid MediaFileId { get; set; }
        public MediaFile MediaFile { get; set; }
        public byte? SequenceOrder { get; set; }
        public string UrlDirection { get; set; }
        public byte StatusDirection { get; set; }
        public string NameDirection { get; set; }
    }
}
