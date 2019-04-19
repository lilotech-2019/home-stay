using System.Collections.Generic;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Data.Models
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public virtual ICollection<RoomOrder> RoomOrders { get; set; }
    }
}