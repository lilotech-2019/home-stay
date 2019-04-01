using Outsourcing.Data.Models.HMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Labixa.Areas.HMSAdmin.ViewModels
{
    public class CostOrderEditModel
    {
        public CostOrderEditModel()
        {
            ListCostOrderItemEditModels = new List<CostOrderItemEditModel>();

        }
        public CostOrder CostOrderModel { get; set; }
        public List<CostOrderItemEditModel> ListCostOrderItemEditModels { get; set; }
        public IEnumerable<SelectListItem> ListHotels { get; set; }
    }
    public class CostOrderItemEditModel
    {
        public CostOrderItem CostOrderItemModel { get; set; }
        public IEnumerable<SelectListItem> ListCosts { get; set; }
    }
}