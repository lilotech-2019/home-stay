using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryParentId { get; set; }


        /// <summary>
        /// URL  SEO friendly
        /// </summary>
        public string Slug { get; set; }    

        public virtual ICollection<ProductCategoryMapping> ProductCategoryMappings { get; set; }

        //public string Test { get; set; }
    }
}
