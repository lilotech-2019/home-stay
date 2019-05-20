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