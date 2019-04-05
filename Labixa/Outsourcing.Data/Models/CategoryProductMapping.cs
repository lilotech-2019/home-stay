using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    class CategoryProductMapping :BaseEntity
    {


        public ICollection<Product> Products { get; set; }
        public ICollection<ProductCategory> ProductCategorys { get; set; }
    }
}
