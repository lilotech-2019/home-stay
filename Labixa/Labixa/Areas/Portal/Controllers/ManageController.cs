using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Labixa.Areas.Portal.ViewModels.User;
using Outsourcing.Data;

namespace Labixa.Areas.Portal.Controllers
{
    public class ManageController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        //
        // GET: /Portal/Manage/
        public ActionResult Index()
        {
            var users = (from user in _context.Users
                select new
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    RoleNames = (from userRole in user.Roles
                        join role in _context.Roles on userRole.RoleId
                            equals role.Id
                        select role.Name).ToList()
                }).ToList().Select(p => new UserIndexViewModels()

            {
                UserId = p.UserId,
                Username = p.Username,
                Email = p.Email,
                Role = string.Join(",", p.RoleNames)
            });
            return View(users);
        }
    }
}