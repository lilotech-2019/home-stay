using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Outsourcing.Data.Models;

namespace Outsourcing.Data.Migrations
{
    public class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            #region Create Roles

            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roles = new List<string>(new[] {"Admin", "Shipment", "Mod", "User", "SupAdmin"});
            foreach (var role in roles)
            {
                if (!_roleManager.RoleExists(role))
                {
                    _roleManager.Create(new IdentityRole(role));
                }
            }

            #endregion

            #region Create Users

            _userManager = new UserManager<User>(new UserStore<User>(context));
            var users = new List<User>
            {
                new User {Email = "admin@homestay.com", UserName = "admin", FirstName = "Company", LastName = "Admin"},
            };

            foreach (var user in users)
            {
                if (_userManager.FindByName(user.UserName) != null) continue;
                var password = user.UserName + "pw";
                var result = _userManager.Create(user, password);
                if (!result.Succeeded) continue;
                var currentUser = _userManager.FindByName(user.UserName);
                _userManager.AddToRole(currentUser.Id, "Admin");
                _userManager.AddToRole(currentUser.Id, "User");
            }

            #endregion

            #region Blog Categories

            if (context.BlogCategories.AsNoTracking().Any() == false)
            {
                context.BlogCategories.Add(new BlogCategories
                {
                    Name = "None",
                    Slug = "none",
                    Status = true,
                    IsStaticPage = false,
                    DisplayOrder = 9999,
                    Description = "Danh Mục Con Của None Sẽ Là Danh Mục Cấp 1"
                });
                context.BlogCategories.Add(new BlogCategories
                {
                    Name = "Trang Tĩnh",
                    Slug = "trang-tinh",
                    Status = true,
                    IsStaticPage = true
                });
                context.BlogCategories.Add(new BlogCategories
                {
                    Name = "Tin tức",
                    Slug = "tin-tuc",
                    Status = true,
                    IsStaticPage = false
                });
                context.BlogCategories.Add(new BlogCategories
                {
                    Name = "Sự kiện",
                    Slug = "su-kien",
                    Status = true,
                    IsStaticPage = false
                });
                context.SaveChanges();
            }

            #endregion

            #region Product Categories

            if (context.ProductCategories.AsNoTracking().Any() == false)
            {
                context.ProductCategories.Add(new ProductCategory
                {
                    Name = "DanhMuc1",
                    Slug = "danh-muc-1",
                    Deleted = false
                });
                context.ProductCategories.Add(new ProductCategory
                {
                    Name = "DanhMuc2",
                    Slug = "danh-muc-2",
                    Deleted = false
                });
                context.SaveChanges();
            }

            #endregion

            #region Staffs

            if (context.Staffs.AsNoTracking().Any() == false)
            {
                context.Staffs.Add(new Staff
                {
                    Name = "Trương Long",
                    Phone = "0922007855",
                    Type = 1,
                    Skype = "longt711",
                    Yahoo = "longt711",
                    Deleted = false
                });
                context.Staffs.Add(new Staff
                {
                    Name = "Ngọc Linh",
                    Phone = "0922007855",
                    Type = 2,
                    Skype = "longt711",
                    Yahoo = "longt711",
                    Deleted = false
                });
                context.Staffs.Add(new Staff
                {
                    Name = "Truong Long",
                    Phone = "0922007855",
                    Type = 3,
                    Skype = "longt711",
                    Yahoo = "longt711",
                    Deleted = false
                });
                context.Staffs.Add(new Staff
                {
                    Name = "Truong Long",
                    Phone = "0922007855",
                    Type = 4,
                    Skype = "longt711",
                    Yahoo = "longt711",
                    Deleted = false
                });
                context.SaveChanges();
            }

            #endregion

            #region Website Attributes

            if (context.WebsiteAtributes.AsNoTracking().Any() == false)
            {
                context.WebsiteAtributes.Add(new WebsiteAtribute
                {
                    Name = "Labixa.PopupWebsite",
                    IsPublic = true,
                    Deleted = false
                });
                context.SaveChanges();
            }

            #endregion
        }
    }
}