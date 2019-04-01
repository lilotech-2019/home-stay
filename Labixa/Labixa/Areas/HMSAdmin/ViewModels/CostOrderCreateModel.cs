using Outsourcing.Data.Models.HMS;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Labixa.Areas.HMSAdmin.ViewModels
{
    public class CostOrderCreateModel
    {
        public CostOrder CostOrderModel { get; set; }
        public IEnumerable<SelectListItem> ListHotels { get; set; }

    }
}