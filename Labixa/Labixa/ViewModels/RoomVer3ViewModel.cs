using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Outsourcing.Data.Models;

namespace Labixa.ViewModels
{
    public class RoomDetailViewModel
    {
        public IEnumerable<Room> RelatedRooms { get; set; }

        //public IEnumerable<Rooms> RelatedRoomLong { get; set; }

        public Room Room { get; set; }
        public IEnumerable<RoomAsset> RoomAssets { get; set; }
    }
}