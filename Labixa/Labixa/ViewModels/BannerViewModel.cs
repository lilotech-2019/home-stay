using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Outsourcing.Data.Models;

namespace Labixa.ViewModels
{
    public class BannerViewModel
    {
        public WebsiteAttributes bannerLogo { get; set; }
        public WebsiteAttributes bannerTitle { get; set; }
        public WebsiteAttributes bannerImage { get; set; }

    }
}