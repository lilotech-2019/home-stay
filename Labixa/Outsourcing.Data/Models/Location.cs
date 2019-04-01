using System.Collections.Generic;

namespace Outsourcing.Data.Models
{
    public class Location : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public int Percent { get; set; }
        public bool isDelete { get; set; }
        public virtual ICollection<InventoryLog> InventoryLog { get; set; }

    }
}
