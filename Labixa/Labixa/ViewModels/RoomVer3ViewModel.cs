using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Outsourcing.Data.Models;
using Outsourcing.Data.Models.HMS;

namespace Labixa.ViewModels
{
    public class RoomVer3ViewModel
    {
        public RoomVer3ViewModel()
        {
            RelatedRoom = new List<Room>();
            RelatedRoomLong = new List<Room>();
        }

        public IEnumerable<Room> RelatedRoom { get; set; }

        public IEnumerable<Room> RelatedRoomLong { get; set; }

        public Room listRoom { get; set; }



    }
}