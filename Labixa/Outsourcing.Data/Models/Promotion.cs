using System.Collections.Generic;

namespace Outsourcing.Data.Models
{
    public class Promotion :BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public int Percent { get; set; }
        public bool isDelete { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
