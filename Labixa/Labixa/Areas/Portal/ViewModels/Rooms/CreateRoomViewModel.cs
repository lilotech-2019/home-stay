using System.Collections.Generic;
using Outsourcing.Data.Models;

namespace Labixa.Areas.Portal.ViewModels.Rooms
{
    public class CreateRoomViewModel
    {
        public IEnumerable<Asset> Assets { get; set; }
        public Room Room { get; set; }
    }
}