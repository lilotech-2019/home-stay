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
        public List<WebsiteAttribute> websiteAttribute { get; set; }
        public List<Blog> blog { get; set; }
        public List<Product> product { get; set; }
        public IEnumerable<Room> roomHome { get; set; }

        public IEnumerable<Blog> blogHome { get; set; }

        public IEnumerable<WebsiteAttribute> imageHome1 { get; set; }
        public IEnumerable<WebsiteAttribute> imageHome2 { get; set; }
        public IEnumerable<WebsiteAttribute> imageHome3 { get; set; }


    }
}