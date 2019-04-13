using System.Collections.Generic;

namespace Outsourcing.Data.Models.HMS
{
 
    public class RoomUtilities : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public bool IsActived { get; set; }
        public string Noted { get; set; }

        public string ControlType { get; set; }

        public virtual ICollection<RoomUtilityRoomMappings> RoomUtilityRoomMappings { get; set; }

        public bool Deleted { get; set; }

    }
}
