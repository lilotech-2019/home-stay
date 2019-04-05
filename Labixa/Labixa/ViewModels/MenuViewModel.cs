using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Outsourcing.Data.Models;

namespace Labixa.ViewModels
{
    public class MenuViewModel
    {
        public WebsiteAttributes pageLogo { get; set; }
        public WebsiteAttributes pageTitle { get; set; }
        public WebsiteAttributes pageSlogan { get; set; }
    }
}