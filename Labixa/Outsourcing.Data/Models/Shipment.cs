using System;
using System.Collections.Generic;

namespace Outsourcing.Data.Models
{
    public class Shipment : BaseEntity
    {
        public int UserId { get;set;}
        public Double Fee { get; set; }
        public DateTime Deadline { get; set; }
        public string Note { get; set; }
        public string Description{ get; set; }
        public bool isDelete { get; set; }
        public virtual ICollection<Order> Order { get; set; } 
        public virtual User User { get; set; }
    }
}
