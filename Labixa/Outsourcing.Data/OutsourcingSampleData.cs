//using System.Collections.Generic;
//using System.Data.Entity;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Outsourcing.Data.Models;


//namespace Outsourcing.Data
//{
//    public class OutsourcingSampleData : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
//    {
//        private UserManager<User> _userManager;
//        private RoleManager<IdentityRole> _roleManager;

//        protected override void Seed(ApplicationDbContext context)
//        {
//            _userManager = new UserManager<User>(new UserStore<User>(context));
//            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

//            //Create User

//            var users = new List<User>() {
//                  new User(){ UserName = "admin", FirstName = "Company", LastName = "Admin",RoleId = SystemRoles.Role01},
//                new User(){ UserName = "longt", FirstName = "Jackie", LastName = "Long",RoleId = SystemRoles.Role01}};
//            foreach (var user in users)
//            {
//                if (_userManager.FindByName(user.UserName) == null)
//                {
//                    _userManager.Create(user, "123456");
//                }
//            }


//            //Create Roles
//            var listRoles = new List<string>(new string[] { "Admin","Shipment","Mod","User","SupAdmin" });
//            foreach (var role in listRoles)
//            {
//                if (!_roleManager.RoleExists(role))
//                {
//                    _roleManager.Create(new IdentityRole(role));
//                }
//            }

//            context.BlogCategories.Add(new BlogCategories() { Name = "None", Slug = "none", Status = true, IsStaticPage = false,DisplayOrder=9999,Description="Danh Mục Con Của None Sẽ Là Danh Mục Cấp 1" });
//            context.BlogCategories.Add(new BlogCategories() { Name = "Trang Tĩnh", Slug = "trang-tinh", Status = true, IsStaticPage = true });
//            context.BlogCategories.Add(new BlogCategories() { Name = "Tin tức", Slug = "tin-tuc", Status = true, IsStaticPage = false});
//            context.BlogCategories.Add(new BlogCategories() { Name = "Sự kiện", Slug = "su-kien", Status = true, IsStaticPage = false });

//            context.ProductCategories.Add(new ProductCategory() { Name = "DanhMuc1", Slug = "danh-muc-1", Deleted = false });
//            context.ProductCategories.Add(new ProductCategory() { Name = "DanhMuc2", Slug = "danh-muc-2", Deleted = false });
//            context.Staffs.Add(new Staff() { Name="Trương Long",Phone="0922007855",Type=1,skype="longt711",Yahoo="longt711",Deleted=false });
//            context.Staffs.Add(new Staff() { Name = "Ngọc Linh", Phone = "0922007855", Type = 2, skype = "longt711", Yahoo = "longt711", Deleted = false });
//            context.Staffs.Add(new Staff() { Name = "Truong Long", Phone = "0922007855", Type = 3, skype = "longt711", Yahoo = "longt711", Deleted = false });
//            context.Staffs.Add(new Staff() { Name = "Truong Long", Phone = "0922007855", Type = 4, skype = "longt711", Yahoo = "longt711", Deleted = false });
            
//            context.WebsiteAttributes.Add(new WebsiteAttributes() { Name = "Labixa.PopupWebsite", IsPublic = true, Deleted = false });
//            context.SaveChanges();
//        }

//    }
//}