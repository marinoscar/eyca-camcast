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
            return Redirect("/Home/Thanks");
        }

        public ActionResult List()
        {
            var contacts = _repo.GetActiveContacts();
            return View(contacts);
        }

        public ActionResult UpdateAll(string ids)
        {
            var idList = ids.Split(",".ToArray()).ToList();
            _repo.UpdateAllContactsAsDisabled(idList);
            return RedirectToAction("List");
        }
    }
}