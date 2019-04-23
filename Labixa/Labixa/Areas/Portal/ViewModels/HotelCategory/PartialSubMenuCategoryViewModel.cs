using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labixa.Areas.Portal.ViewModels.HotelCategory
{
    public class PartialSubMenuCategoryViewModel
    {
        public IEnumerable<Outsourcing.Data.Models.HotelCategory> HotelCategories { get; set; }
        public int Count { get; set; }

    }
}