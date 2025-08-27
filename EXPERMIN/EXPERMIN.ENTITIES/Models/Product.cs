using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.ENTITIES.Models
{
    public class Product : BaseEntity
    {
        public Guid MediaFileId { get; set; }
        public MediaFile MediaFile { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public byte? Order { get; set; } = 255;
        public byte Status { get; set; } // activo = 1, oculto = 2
        public DateTime PublicationDate { get; set; } //se actualiza cuando lo pones a estado "Activo"
    }
}
