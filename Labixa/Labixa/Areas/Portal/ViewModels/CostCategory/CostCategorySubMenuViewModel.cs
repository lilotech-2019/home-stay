using System.Collections.Generic;

namespace Labixa.Areas.Portal.ViewModels.CostCategory
{
    public class CostCategorySubMenuViewModel
    {
        public IEnumerable<Outsourcing.Data.Models.HMS.CostCategory> CostCategories { get; set; }
        public int Count { get; set; }

    }
}