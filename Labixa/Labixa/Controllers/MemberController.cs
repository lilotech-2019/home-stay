using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Outsourcing.Data.Models;
using Outsourcing.Service;
using Labixa.Areas.Common;
using Labixa.ViewModels;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Labixa.Models;
using Labixa.Areas.Admin.ViewModel;

namespace Labixa.Controllers
{
    public class MemberController : Controller
    {
        #region fields
        private readonly IAppliantService _appliantService;
        private readonly IStockService _stockService;
        private readonly IPointService _pointService;
        private readonly IOrderService _orderService;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        Outsourcing.Data.OutsourcingEntities db = new Outsourcing.Data.OutsourcingEntities();
        #endregion

        public MemberController(IAppliantService _appliantService, IStockService _stockService, 
            IPointService _pointService, IOrderService _orderService)
        {
            this._orderService = _orderService;
            this._appliantService = _appliantService;
            this._stockService = _stockService;
            this._pointService = _pointService;
            _userManager = new UserManager<User>(new UserStore<User>(db));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        }

        public ActionResult Dashboard()
        {
            Appliant model = _appliantService.GetAppliant()
                .Where(a => a.Year_Graduated.Equals(User.Identity.Name))
                .FirstOrDefault();
            if (model == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        //
        // GET: /Member/
        public ActionResult Index()
        {
            Appliant model = _appliantService.GetAppliant().Where(a => a.Year_Graduated.Equals(User.Identity.Name)).FirstOrDefault();
            if(model == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        public ActionResult Detail(int? Id)
        {
            Appliant model = _appliantService.GetAppliantById((int)Id);
            if(model == null)
            {
                return RedirectToAction("Manage", "AppliantAdmin");
            }
            return View("Dashboard", model);
        }

        //
        // GET: /Member/Edit
        public ActionResult Edit()
        {
            Appliant model = _appliantService.GetAppliant().Where(a => a.Year_Graduated.Equals(User.Identity.Name)).FirstOrDefault();
            if (model == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        public async Task<ActionResult> ChangePassword(AdminCreateModel model)
        {
            var user = _userManager.FindByName(model.UserName);
            if (user != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(model.Password);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var app = _appliantService.GetAppliant().Where(a => a.Username.Equals(model.UserName)).FirstOrDefault();
                    app.Password = model.Password;
                    _appliantService.EditAppliant(app);
                    return RedirectToAction("Manage");
                }
                else
                {
                }
            }
            return this.Content("1");
        }

        //
        // POST
        [HttpPost]
        public ActionResult Edit(Appliant model)
        {
            if (model == null)
            {
                return RedirectToAction("Edit");
            }
            if (model != null)
            {
                _appliantService.EditAppliant(model);
                AdminCreateModel tmodel = new AdminCreateModel() { UserName = model.Year_Graduated, Password = model.Password };
                ChangePassword(tmodel);
            }
            return RedirectToAction("Index");
        }

        public ActionResult ChangePassword(String newPassword)
        {

            return PartialView();
        }

        //
        //Kendo Grid import Stock
        public ActionResult EditStock()
        {
            Appliant branch = _appliantService.GetAppliant().Where(o => o.Year_Graduated.Equals(User.Identity.Name)).FirstOrDefault();
            int userId = branch.Id;
            var stocks = this.DeserializeObject<IEnumerable<StockViewModel>>("models");
            if (stocks != null)
            {
                foreach(var stock in stocks)
                {
                    _stockService.CreateStock(new StockHistory()
                    {
                        BranchId = userId,
                        CreateDate = stock.CreateDate,
                        Value = stock.Value,
                        Description = stock.Description
                    });
                    branch.Numer_3 += stock.Value;
                }
                _appliantService.EditAppliant(branch);
            }
            return this.Jsonp(stocks);
        }

        //
        //Kendo Grid read data
        public ActionResult ReadStock()
        {
            int userId;
            try
            {
                userId = _appliantService.GetAppliant()
                .Where(a => a.Year_Graduated.Equals(User.Identity.Name)).FirstOrDefault().Id;
            }
            catch
            {
                return null;
            }
            var list = _stockService.GetStock()
                .Where(s => s.BranchId == userId)
                .OrderByDescending(stock => stock.CreateDate);
            return this.Jsonp(list.ToList());
        }

        //
        // GET: /Member/StockHistory
        public ActionResult StockHistory()
        {
            int userId;
            try
            {
                userId = _appliantService.GetAppliant()
                .Where(a => a.Year_Graduated.Equals(User.Identity.Name)).FirstOrDefault().Id;
            }
            catch
            {
                return RedirectToAction("Index");
            }
            var list = _stockService.GetStock()
                .Where(s => s.BranchId == userId).Select(o => o.Value).Sum();
            return View(list);
        }

        //
        //Kendo Grid read data
        public ActionResult ReadPoint()
        {
            int userId;
            try
            {
                userId = _appliantService.GetAppliant()
                .Where(a => a.Year_Graduated.Equals(User.Identity.Name)).FirstOrDefault().Id;
            }
            catch
            {
                return null;
            }
            var list = _pointService.GetPoint()
                .Where(p => p.BranchId == userId)
                .OrderByDescending(point => point.CreateDate);
            return this.Jsonp(list.ToList());
        }

        //
        // GET: /Member/PointHistory
        public ActionResult PointHistory()
        {
            int userId;
            try
            {
                userId = _appliantService.GetAppliant()
                .Where(a => a.Year_Graduated.Equals(User.Identity.Name)).FirstOrDefault().Id;
            }
            catch
            {
                return null;
            }
            var list = _pointService.GetPoint()
                .Where(p => p.BranchId == userId)
                .Select(o => o.Value).Sum();
            return View(list);
        }

        public JsonResult ReadDiagram(int id)
        {
            //error list
            List<String> from = new List<string>();
            List<String> to = new List<String>();

            int userId;
            int userId2 = 0;
            try
            {
                userId2 = _appliantService.GetAppliant()
                .Where(a => a.Year_Graduated.Equals(User.Identity.Name)).FirstOrDefault().Id;
            }
            catch
            {
            }
            userId = (id != 0) ? id : userId2;
            var listBranch = _appliantService.GetAppliant();
            Appliant appliant = _appliantService.GetAppliantById(userId);
            DiagramViewModel model = new DiagramViewModel() {
                Name = appliant.Username,
                Address = appliant.City,
                CreateDate = appliant.CreateDate.ToString("dd/MM/yyyy"),
                Stock = appliant.Numer_3,
                Point = appliant.Numer_2,
            };
            Queue<DiagramViewModel> queue = new Queue<DiagramViewModel>();
            Queue<String> queueId = new Queue<String>();
            List<String> visited = new List<String>();
            queue.Enqueue(model);
            queueId.Enqueue(appliant.Username);
            visited.Add(appliant.Username);
            while(queue.Count > 0)
            {
                String branchId = queueId.Dequeue();
                DiagramViewModel node = queue.Dequeue();
                var listNode = listBranch
                    .Where(a => a.Placement != null && a.Placement.Equals(branchId))
                    .OrderBy(ap => ap.CreateDate);
                foreach(Appliant tmp in listNode)
                {
                    if(visited.Where(k => k.Equals(tmp.Year_Graduated)).Count() == 0){
                        DiagramViewModel tnode = new DiagramViewModel()
                        {
                            Name = tmp.Username,
                            Address = tmp.City,
                            CreateDate = tmp.CreateDate.ToString("dd/MM/yyyy"),
                            Point = tmp.Numer_2,
                            Stock = tmp.Numer_3,
                        };
                        node.Items.Add(tnode);
                        queue.Enqueue(tnode);
                        queueId.Enqueue(tmp.Username);
                        visited.Add(tmp.Username);
                    }
                    else
                    {
                        from.Add(branchId);
                        to.Add(tmp.Username);
                    }
                }
            }

            List<DiagramViewModel> list = new List<DiagramViewModel>();
            list.Add(model);
            Session["from"] = from;
            Session["to"] = to;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Member/OrganizationChart
        public ActionResult OrganizationChart(int? id)
        {
            ViewBag.userId = id.HasValue ? Convert.ToInt32(id) : 0;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Contact(String Message)
        {
            Order message = new Order();
            message.CustomerName = User.Identity.Name;
            message.DateCreated = DateTime.Now;
            message.CustomerEmail = Message;
            int userId;
            try
            {
                userId = _appliantService.GetAppliant()
                .Where(a => a.Year_Graduated.Equals(User.Identity.Name)).FirstOrDefault().Id;
            }
            catch
            {
                return RedirectToAction("Index");
            }
            message.OrderTotal = userId;
            _orderService.CreateOrder(message);
            return RedirectToAction("Contact");
        }
        
        public ActionResult ReadDiagramE(int id)
        {
            List<GroupViewModel> list = new List<GroupViewModel>();
            int userId;
            int userId2 = 0;
            try
            {
                userId2 = _appliantService.GetAppliant()
                .Where(a => a.Year_Graduated.Equals(User.Identity.Name)).FirstOrDefault().Id;
            }
            catch
            {
            }
            userId = (id != 0) ? id : userId2;
            var listBranch = _appliantService.GetAppliant();
            Appliant appliant = _appliantService.GetAppliantById(userId);
            GroupViewModel model = new GroupViewModel()
            {
                Username = appliant.Username,
                Numer_3 = appliant.Numer_3,
                Numer_2 = appliant.Numer_2,
            };
            Queue<GroupViewModel> queue = new Queue<GroupViewModel>();
            Queue<String> queueId = new Queue<String>();
            queue.Enqueue(model);
            queueId.Enqueue(appliant.Username);
            while (queue.Count > 0)
            {
                String branchId = queueId.Dequeue();
                GroupViewModel node = queue.Dequeue();
                list.Add(node);
                var listNode = listBranch
                    .Where(a => a.Placement != null && a.Placement.Equals(branchId));
                foreach (Appliant tmp in listNode)
                {
                    GroupViewModel tnode = new GroupViewModel()
                    {
                        Username = tmp.Username,
                        Numer_2 = tmp.Numer_2,
                        Numer_3 = tmp.Numer_3,
                    };
                    queue.Enqueue(tnode);
                    queueId.Enqueue(tmp.Username);
                }
            }
            return this.Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PlacementChart()
        {
            Appliant branch = _appliantService.GetAppliant().Where(o => o.Year_Graduated.Equals(User.Identity.Name)).FirstOrDefault();
            ViewBag.phone = branch.String_1;
         
            return View();
        }

        public ActionResult ReadPhone()
        {
            Appliant branch = _appliantService.GetAppliant().Where(o => o.Year_Graduated.Equals(User.Identity.Name)).FirstOrDefault();
            var list = _appliantService.GetAppliant().Where(p => p.String_1.Equals(branch.String_1));
            var listGroup = new List<GroupViewModel>();
            foreach (var item in list)
            {
                GroupViewModel group = new GroupViewModel();
                group.Username = item.Username;
                group.Numer_2 = item.Numer_2;
                group.Numer_3 = item.Numer_3;
                listGroup.Add(group);
            }
            return this.Json(listGroup.ToList(), JsonRequestBehavior.AllowGet);
        }

        //
        //Get
        public ActionResult Contact()
        {
            return View();
        }
	}
}