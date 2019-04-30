using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Outsourcing.Data.Models;

namespace Labixa.ViewModels
{
    public class BannerViewModel
    {
        public WebsiteAtribute bannerLogo { get; set; }
        public WebsiteAtribute bannerTitle { get; set; }
        public WebsiteAtribute bannerImage { get; set; }

    }
}