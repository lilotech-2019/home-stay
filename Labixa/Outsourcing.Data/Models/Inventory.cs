using System;
using System.Collections.Generic;

namespace Outsourcing.Data.Models
{
    public class Inventory :BaseEntity
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        //public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Double Price { get; set; }
        public bool IsImport { get; set; }
        public bool IsDelete{ get; set; }

        //[ForeignKey("ProductId")]
        //public virtual Product Product { get; set; }
        public virtual ICollection<InventoryLog> InventoryLog { get; set; }


    }
}
