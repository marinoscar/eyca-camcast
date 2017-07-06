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

        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs("POST")]
        public ActionResult Add(Contact contact )
        {
            var repo = new ItemRepository(new TableClient<Item>("items"));
            repo.AddContact(contact);
            return Redirect("/Home");
        }
    }
}