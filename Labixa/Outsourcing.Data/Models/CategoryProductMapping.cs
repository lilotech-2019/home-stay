using System.Collections.Generic;

namespace Outsourcing.Data.Models
{
    class CategoryProductMapping :BaseEntity
    {


        public ICollection<Product> Products { get; set; }
        public ICollection<ProductCategory> ProductCategorys { get; set; }
    }
}
