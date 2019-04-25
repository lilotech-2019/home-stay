using System.Collections.Generic;

namespace Labixa.Areas.Portal.ViewModels.HotelCategory
{
    public class PartialSubMenuCategoryViewModel
    {
        public IEnumerable<Outsourcing.Data.Models.HotelCategory> HotelCategories { get; set; }
        public int Count { get; set; }

    }
}