using Outsourcing.Data.Models;

namespace Labixa.Areas.Portal.ViewModels.Rooms
{
    public class RoomDetailsSubMenuViewModel
    {
        public PagedList.IPagedList<RoomOrder> RoomOrders { get; set; }
        public Room Room { get; set; }

    }
}