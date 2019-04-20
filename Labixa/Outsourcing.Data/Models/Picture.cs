using System.Collections.Generic;


namespace Outsourcing.Data.Models
{
    public class Picture :BaseEntity
    {
        public string Binary { get; set; }
        public string MimeType { get; set; }
        public string Url { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<ProductPictureMapping> ProductPictureMapping { get; set; } 
    }
}
