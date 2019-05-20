using System.Collections.Generic;
using Outsourcing.Data.Models;

namespace Labixa.Areas.Portal.ViewModels.Rooms
{
    public class PartialSubMenuCategoryViewModel
    {
        public IEnumerable<RoomOrder> RoomOrders  { get; set; }
        public int Count { get; set; }
    }
}