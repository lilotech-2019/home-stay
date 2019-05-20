using Outsourcing.Data.Models;

namespace Labixa.Areas.Portal.ViewModels.Rooms
{
    public class RoomDetailsSubMenuViewModel
    {
        public PagedList.IPagedList<Outsourcing.Data.Models.HMS.RoomOrder> RoomOrders { get; set; }
        public Room Room { get; set; }

    }
}