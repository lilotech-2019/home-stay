using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Outsourcing.Data.Models;
using Outsourcing.Data.Models.HMS;

namespace Labixa.ViewModels
{
    public class IndexViewModel
    {
        public List<WebsiteAttributes> websiteAttribute { get; set; }
        public List<Blogs> blog { get; set; }
        public List<Product> product { get; set; }
        public IEnumerable<Rooms> roomHome { get; set; }

        public IEnumerable<Blogs> blogHome { get; set; }

        public IEnumerable<WebsiteAttributes> imageHome1 { get; set; }
        public IEnumerable<WebsiteAttributes> imageHome2 { get; set; }
        public IEnumerable<WebsiteAttributes> imageHome3 { get; set; }


    }
}