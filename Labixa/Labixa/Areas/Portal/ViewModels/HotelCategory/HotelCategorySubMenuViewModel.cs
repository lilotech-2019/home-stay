using System.Collections.Generic;

namespace Labixa.Areas.Portal.ViewModels.HotelCategory
{
    public class HotelCategorySubMenuViewModel
    {
        public IEnumerable<Outsourcing.Data.Models.HotelCategory> HotelCategories { get; set; }
        public int Count { get; set; }

    }
}