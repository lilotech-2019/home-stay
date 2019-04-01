using Outsourcing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Labixa.Areas.Admin.ViewModel
{
    public class InventoryModels
    {
        public InventoryModels()
        {
            Location = new List<SelectListItem>();
        }
        public Product product { get; set; }
        public InventoryLog inventory { get; set; }
        public int locationId { get; set; }

        public IEnumerable<SelectListItem> Location { get; set; }

    }
}