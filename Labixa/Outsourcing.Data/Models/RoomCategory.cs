using System.Collections.Generic;

namespace Outsourcing.Data.Models.HMS
{
    public class RoomCategory
    {
        public virtual ICollection<Room> Rooms { get; set; }
    }
}