using System.Collections.Generic;

namespace Labixa.Areas.Portal.ViewModels.Rooms
{
    public class PartialSubMenuCategoryViewModel
    {
        public IEnumerable<Outsourcing.Data.Models.HMS.RoomOrder> RoomOrders  { get; set; }
        public int Count { get; set; }
    }
}