using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class ProductCategoryMapping :BaseEntity
    {
        public string Value { get; set; }
        public string Note { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
