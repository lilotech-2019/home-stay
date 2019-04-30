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
        public List<Blog> blog { get; set; }
        public List<Product> product { get; set; }
        public IEnumerable<Room> roomHome { get; set; }

        public IEnumerable<Blog> blogHome { get; set; }      

        public IEnumerable<WebsiteAttributes> imageHome1 { get; set; }
        public IEnumerable<WebsiteAttributes> imageHome2 { get; set; }
        public IEnumerable<WebsiteAttributes> imageHome3 { get; set; }
        public List<SlideViewModel> Slider { get; set; }

        public class SlideViewModel
        {
            public string ImageURL { get; set; }
            public string Style { get; set; }
            public string Title { get; set; }
            public string TitleEnglish { get; set; }
            public string Caption { get; set; }
            public string CaptionEnglish { get; set; }
        }


    }
}