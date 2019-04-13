using System.Collections.Generic;

namespace Outsourcing.Data.Models.HMS
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Number { get; set; }

        public virtual ICollection<RoomOrder> RoomOrders { get; set; }
    }
}