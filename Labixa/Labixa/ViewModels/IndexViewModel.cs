using System.Collections.Generic;
using Outsourcing.Data.Models;

namespace Labixa.ViewModels
{
    public class IndexViewModel
    {
        public List<WebsiteAtribute> WebsiteAttribute { get; set; }
        public List<Blog> Blog { get; set; }

        public IEnumerable<Room> RoomHome { get; set; }

        public IEnumerable<Blog> BlogHome { get; set; }

        public IEnumerable<WebsiteAtribute> ImageHome1 { get; set; }
        public IEnumerable<WebsiteAtribute> ImageHome2 { get; set; }
        public IEnumerable<WebsiteAtribute> ImageHome3 { get; set; }
        public List<SlideViewModel> Slider { get; set; }

        public class SlideViewModel
        {
            public string ImageUrl { get; set; }
            public string Style { get; set; }
            public string Title { get; set; }
            public string TitleEnglish { get; set; }
            public string Caption { get; set; }
            public string CaptionEnglish { get; set; }
        }
    }
}