using System;

namespace Outsourcing.Data.Models
{
    public class InventoryLog:BaseEntity
    {
        public string Name { get; set; }
        public string Description{ get; set; }
        public string Note { get; set; }
        public int Quantity { get; set; }
        public Double Price { get; set; }
        public DateTime DateCreated{ get; set; }
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public bool IsDelete { get; set; }
        public bool IsImport { get; set; }
        public bool IsChangeLocation { get; set; }

        public virtual Location Location { get; set; }
        public virtual Product Product { get; set; }
        public virtual Inventory Inventory { get; set; }

    }
}
