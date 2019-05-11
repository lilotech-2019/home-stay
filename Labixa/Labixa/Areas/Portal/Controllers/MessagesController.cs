using Outsourcing.Data.Models;
using Outsourcing.Service.Portal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Labixa.Areas.Portal.Controllers
{
    public class MessagesController : Controller
    {
        #region Fields
        private readonly IMessageService _messageService;
        private readonly ICustomerService _customerService;
        #endregion

        #region Ctor
        public MessagesController(IMessageService messageService, ICustomerService customerService)
        {
            _messageService = messageService;
            _customerService = customerService;
        }
        #endregion

        #region Index
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var messages = await _messageService.FindAll().AsNoTracking().ToListAsync();
            return View(messages);
        }
        #endregion

        #region Details
        /// <summary>
        /// Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message messages = _messageService.FindById((int)id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            return View(messages);
        }
        #endregion

        #region Create
        /// <summary>
        /// Create - GET
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(_customerService.FindSelectList(), "Id", "Name");
            return View();
        }

        /// <summary>
        /// Create - POST
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Message message)
        {
            if (ModelState.IsValid)
            {
                _messageService.Create(message);
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(_customerService.FindSelectList(), "Id", "Name");
            return View(message);
        }
        #endregion

        #region Edit
        /// <summary>
        /// Edit - GET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var message = _messageService.FindById((int)id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(_customerService.FindSelectList(message.CustomerId), "Id", "Name", message.CustomerId);
            return View(message);
        }

        /// <summary>
        /// Edit - POST
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Message message)
        {
            if (ModelState.IsValid)
            {
                _messageService.Edit(message);
                return RedirectToAction("Index");
            }
            return View(message);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var message = _messageService.FindById((int)id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        /// <summary>
        /// DeleteConfirmed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var message = _messageService.FindById(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            _messageService.Delete(message);
            return RedirectToAction("Index");
        }
        #endregion

        #region Reply
        /// <summary>
        /// Reply - GET
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Reply(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var message = _messageService.FindById((int)id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(_customerService.FindSelectList(message.CustomerId), "Id", "Name", message.CustomerId);
            return View(message);
        }

        /// <summary>
        /// Reply - POST
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reply(Message message)
        {
            if (ModelState.IsValid)
            {
                var entity = _messageService.FindById(message.Id);

                entity.Type = MessageType.Replied;                                               
                entity.Answer = message.Answer;
                _messageService.Edit(entity);
                return RedirectToAction("Index");
            }
            return View(message);
        }
        #endregion
    }
}