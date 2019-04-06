using System;
using System.Collections.Generic;

namespace Outsourcing.Data.Models
{
    public class Order : BaseEntity
    {
        public Order()
        {
            DateCreated = DateTime.Now;
        }

        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public int OrderTotal { get; set; }
        public int Status { get; set; }
        public int? ShipmentId { get; set; }
        public Double ShipmentFee { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime Deadline { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual Shipment Shipment { get; set; }
    }
}