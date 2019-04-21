using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Outsourcing.Service.Portal;

namespace Labixa.Areas.Portal.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        /// <summary>
        /// This Method is test new service is working
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var customers = _customerService.FindAll().AsNoTracking()
                            .ToList();
            return View(customers);
        }
    }
}