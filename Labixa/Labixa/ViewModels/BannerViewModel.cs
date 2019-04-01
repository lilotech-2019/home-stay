using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Outsourcing.Data.Models;

namespace Labixa.ViewModels
{
    public class BannerViewModel
    {
        public WebsiteAttribute bannerLogo { get; set; }
        public WebsiteAttribute bannerTitle { get; set; }
        public WebsiteAttribute bannerImage { get; set; }

    }
}