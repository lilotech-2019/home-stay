using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Outsourcing.Data.Models;
using Outsourcing.Data.Models.HMS;

namespace Labixa.Areas.HMSAdmin.ViewModels
{
    public class CostModel
    {
        public CostModel()
        {
            Cost = new Cost();
        }
        public Cost Cost { get; set; }
        public IEnumerable<SelectListItem> ListCostCategory { get; set; }
    }
}