//using System;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Data.Entity.Validation;
//using Outsourcing.Data.Models;
//using Microsoft.AspNet.Identity.EntityFramework;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.ModelConfiguration.Conventions;
//using Outsourcing.Data.Models.HMS;


//namespace Outsourcing.Data
//{
//    public class OutsourcingEntities : IdentityDbContext<User>
//    {

//        public OutsourcingEntities()
//            : base("OutsourcingEntities")
//        {
//        }

//        public DbSet<Blogs> Blogs { get; set; }
//        public DbSet<BlogCategories> BlogCategories { get; set; }
//        public DbSet<Order> Orders { get; set; }
//        public DbSet<OrderItem> OrderItems { get; set; }
//        public DbSet<Picture> Pictures { get; set; }
//        public DbSet<Product> Products { get; set; }
//        public DbSet<ProductAttribute> ProductAttributes { get; set; }
//        public DbSet<ProductAttributeMapping> ProductAttributeMappings { get; set; }
//        public DbSet<ProductCategory> ProductCategories { get; set; }
//        public DbSet<ProductPictureMapping> ProductPictureMappings { get; set; }
//        public DbSet<Staff> Staffs { get; set; }
//        public DbSet<WebsiteAttributes> WebsiteAttributes { get; set; }
//        public DbSet<Colors> Colors { get; set; }
//        public DbSet<Inventory> Inventorys { get; set; }
//        public DbSet<InventoryLog> InventoryLogs { get; set; }
//        public DbSet<Location> Locations { get; set; }
//        public DbSet<Notification> Notifications { get; set; }
//        public DbSet<TypeNotify> TypeNotifys { get; set; }
//        public DbSet<ProductCategoryMapping> ProductCategoryMappings { get; set; }
//        public DbSet<Promotion> Promotions { get; set; }
//        public DbSet<Shipment> Shipments { get; set; }
//        public DbSet<Vendors> Vendors { get; set; }
//        public DbSet<CategoryHotels> CategoryHotels { get; set; }
//        public DbSet<CategoryProducts> CategoryProducts { get; set; }
//        public DbSet<CostCategory> CostCategorys { get; set; }
//        public DbSet<CostOrderItem> CostOrderItems { get; set; }
//        public DbSet<CostOrder> CostOrders { get; set; }
//        public DbSet<Costs> Costs { get; set; }
//        public DbSet<Hotels> Hotels { get; set; }
//        public DbSet<Rooms> Room { get; set; }
//        public DbSet<RoomImageMappings> RoomImageMappings { get; set; }
//        public DbSet<RoomOrders> RoomOrders { get; set; }
//        public DbSet<RoomOrderItems> RoomOrderItems { get; set; }
//        public DbSet<RoomUtilities> RoomUtilitys { get; set; }
//        public DbSet<RoomUtilityRoomMappings> RoomUtilityRoomMappings { get; set; }
//        public DbSet<RoomImages> RoomImages { get; set; }
//        public DbSet<HMSProduct> HMSProduct { get; set; }//a


//        public virtual void Commit()
//        {
//            try
//            {

//                base.SaveChanges();
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }

//        }
//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {

//            base.OnModelCreating(modelBuilder);
//            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

//        }













//    }
//}