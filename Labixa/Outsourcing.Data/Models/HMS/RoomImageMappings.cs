using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models.HMS
{

    public class RoomImageMappings : BaseEntity
    {
        public string Title { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsMainPicture { get; set; }
        public int RoomId { get; set; }
        public int RoomImageId { get; set; }

        public virtual Rooms Room { get; set; }
        public virtual RoomImages RoomImage { get; set; }

    }
}
