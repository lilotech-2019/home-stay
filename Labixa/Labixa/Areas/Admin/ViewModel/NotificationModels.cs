using Outsourcing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Labixa.Areas.Admin.ViewModel
{
    public class NotificationModels
    {
        public NotificationModels()
        {
            ListType = new List<SelectListItem>();
        }
        public Notification notification { get; set; }
        public IEnumerable<SelectListItem> ListType { get; set; }
    }
}