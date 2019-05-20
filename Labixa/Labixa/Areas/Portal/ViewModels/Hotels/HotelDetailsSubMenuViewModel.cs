using System.Collections.Generic;

namespace Labixa.Areas.Portal.ViewModels.Hotels
{
    public class HotelDetailsSubMenuViewModel
    {
        public PagedList.IPagedList<Outsourcing.Data.Models.Room> Rooms { get; set; }
        public int HotelId { get; set; }

    }
}