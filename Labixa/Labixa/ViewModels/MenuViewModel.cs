using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Outsourcing.Data.Models;

namespace Labixa.ViewModels
{
    public class MenuViewModel
    {
        public WebsiteAttribute pageLogo { get; set; }
        public WebsiteAttribute pageTitle { get; set; }
        public WebsiteAttribute pageSlogan { get; set; }
    }
}