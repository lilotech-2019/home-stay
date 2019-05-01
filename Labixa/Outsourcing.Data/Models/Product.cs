using System;
using System.Collections.Generic;

namespace Outsourcing.Data.Models
{
    public class Product : BaseEntity
    {
        public Product()
        {
            LastEditedTime = DateTime.Now;
            DateCreated = DateTime.Now;

        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string CodeName { get; set; }
        public string ColorName { get; set; }
        public string Content { get; set; }
        public int Price { get; set; }
        public int OldPrice { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public DateTime LastEditedTime { get; set; }

        public bool IsHomePage { get; set; }
        public bool IsPublic { get; set; }
        public bool IsNew { get; set; }

        public int Position { get; set; }

        //public int InventoryId { get; set; }
        public int DiscountOfVendor { get; set; }

        public double OrginalPrice { get; set; }


        public virtual ICollection<InventoryLog> InventoryLogs { get; set; }

        //public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<ProductPictureMapping> ProductPictureMappings { get; set; }

        public virtual ICollection<ProductAttributeMapping> ProductAttributeMappings { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<ProductCategoryMapping> ProductCategoryMappings { get; set; }
    }
}