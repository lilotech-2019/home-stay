
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity.EntityFramework;
using Outsourcing.Data.Models;
using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext() : base("ApplicationDbContext", false)
        {
        }


        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategories> BlogCategories { get; set; }
        public DbSet<WebsiteAtribute> WebsiteAtributes { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
     
        public DbSet<HotelCategory> HotelCategories { get; set; }
        public DbSet<CostCategory> CostCategorys { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomImageMappings> RoomImageMappings { get; set; }
        public DbSet<RoomOrder> RoomOrders { get; set; }
        public DbSet<RoomImages> RoomImages { get; set; }
        

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<RoomAsset> RoomAssets { get; set; }
       
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual void Commit()
        {
            try
            {
                SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine(@"Entity of type ""{0}"" in state ""{1}"" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine(@"- Property: ""{0}"", Error: ""{1}""",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}