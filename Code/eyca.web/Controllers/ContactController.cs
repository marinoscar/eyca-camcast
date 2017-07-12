using eyca.core.Data;
using eyca.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eyca.web.Controllers
{
    public class ContactController : Controller
    {
        private ItemRepository _repo;

        public ContactController()
        {
            _repo = new ItemRepository(new TableClient<Item>("items"));
            ViewBag.WelcomeMessage = "Welcome to our EY Digital CoE information Bot!".ToUpperInvariant();
            ViewBag.WelcomeSubtitle = "Would you like to receive more information on our capabilities and see me in action?".ToUpperInvariant();
            ViewBag.WelcomeLink = "/Contact";
            ViewBag.HomeUrl = "/Contact/Home";
        }

        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs("POST")]
        public ActionResult Add(Contact contact )
        {
            _repo.AddContact(contact);
            return Redirect("/Home/Thanks?src=1");
        }

        public ActionResult List()
        {
            var contacts = _repo.GetActiveContacts();
            return View(contacts);
        }

        public JsonResult IsEmpty()
        {
            var contacts = _repo.GetActiveContacts();
            var result = !contacts.Any();
            return Json(result);
        }

        public ActionResult UpdateAll(string ids)
        {
            var idList = ids.Split(",".ToArray()).ToList();
            _repo.UpdateAllContactsAsDisabled(idList);
            return RedirectToAction("List");
        }

        public ActionResult Home()
        {
            return View();
        }
    }
}