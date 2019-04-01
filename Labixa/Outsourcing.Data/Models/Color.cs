using System.Collections.Generic;

namespace Outsourcing.Data.Models
{
    public class Color : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public bool isDelete { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
