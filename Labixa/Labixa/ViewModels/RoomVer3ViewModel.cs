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
            RelatedRoom = new List<Rooms>();
            RelatedRoomLong = new List<Rooms>();
        }

        public IEnumerable<Rooms> RelatedRoom { get; set; }

        public IEnumerable<Rooms> RelatedRoomLong { get; set; }

        public Rooms listRoom { get; set; }



    }
}