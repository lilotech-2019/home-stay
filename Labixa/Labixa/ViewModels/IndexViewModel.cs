using System.Collections.Generic;
using Outsourcing.Data.Models;

namespace Labixa.ViewModels
{
    public class IndexViewModel
    {
        public List<WebsiteAtribute> WebsiteAttribute { get; set; }
        public List<Blog> Blog { get; set; }
        public List<Product> Product { get; set; }
        public IEnumerable<Room> RoomHome { get; set; }

        public IEnumerable<Blog> BlogHome { get; set; }

        public IEnumerable<WebsiteAtribute> imageHome1 { get; set; }
        public IEnumerable<WebsiteAtribute> imageHome2 { get; set; }
        public IEnumerable<WebsiteAtribute> imageHome3 { get; set; }


    }
}