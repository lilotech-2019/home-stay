using System.Collections.Generic;
using System.Web.Mvc;

namespace Labixa.Areas.HMSAdmin.ViewModels.Costs
{
    public class CostModel
    {
        public CostModel()
        {
            Cost = new Outsourcing.Data.Models.HMS.Cost();
        }
        public Outsourcing.Data.Models.HMS.Cost Cost { get; set; }
        public IEnumerable<SelectListItem> ListCostCategory { get; set; }
    }
}