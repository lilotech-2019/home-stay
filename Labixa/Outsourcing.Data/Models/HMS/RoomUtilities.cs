using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models.HMS
{
 
    public class RoomUtilities : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public bool isActived { get; set; }
        public string Noted { get; set; }

        public string ControlType { get; set; }

        public virtual ICollection<RoomUtilityRoomMappings> RoomUtilityRoomMappings { get; set; }

        public bool Deleted { get; set; }

    }
}
