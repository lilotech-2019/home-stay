using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsourcing.Data.Models.HMS
{
    public class HMSProduct : BaseEntity
    {
        public HMSProduct()
        {
            LastEditedTime = DateCreated = DateTime.Now;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string CodeName { get; set; }
        public string ColorName { get; set; }
        public string Content { get; set; }
        public double Price { get; set; }
        public double OldPrice { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastEditedTime { get; set; }

        public bool IsHomePage { get; set; }
        public bool IsPublic { get; set; }
        public bool IsNew { get; set; }
        public bool Deleted { get; set; }
        public int? Position { get; set; }
        public int CategoryProductId { get; set; }
        //public int InventoryId { get; set; }
        public int DiscountOfVendor { get; set; }
        public Double OrginalPrice { get; set; }
        public string UrlImage { get; set; }
        //public int PictureId { get; set; }
        //[ForeignKey("VendorId")]
        //public virtual Vendor Vendor { get; set; }
        //[ForeignKey("LocationId")]
        //public virtual Location Location { get; set; }
        [ForeignKey("CategoryProductId")]
        public virtual CategoryProducts CategoryProduct { get; set; }
        //[ForeignKey("PromotionId")]
        //public virtual Promotion Promotion { get; set; }
        //[ForeignKey("InventoryId")]
        //public virtual Inventory Inventory { get; set; }
        //public virtual ICollection<InventoryLog> InventoryLogs { get; set; }

        //public virtual ProductCategory ProductCategory { get; set; }
        //public virtual ICollection<ProductPictureMapping> ProductPictureMappings { get; set; }
        //public virtual ICollection<ProductAttributeMapping> ProductAttributeMappings { get; set; }
        public virtual ICollection<RoomOrderItem> RoomOrderItems { get; set; }
        //public virtual ICollection<ProductCategoryMapping> ProductCategoryMappings { get; set; }
    }
}
