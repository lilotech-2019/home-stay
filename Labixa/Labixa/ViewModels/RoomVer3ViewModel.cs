using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Outsourcing.Data.Models;
using Outsourcing.Data.Models.HMS;

namespace Labixa.ViewModels
{
    public class RoomDetailViewModel
    {
        

        public IEnumerable<Rooms> RelatedRooms { get; set; }

        //public IEnumerable<Rooms> RelatedRoomLong { get; set; }

        public Rooms Room { get; set; }



    }
}