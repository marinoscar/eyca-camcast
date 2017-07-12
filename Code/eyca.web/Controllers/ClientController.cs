using eyca.core.Data;
using eyca.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eyca.web.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Client client)
        {
            var repo = new ItemRepository(new TableClient<Item>("items"));
            repo.AddClient(client);
            return Redirect("/Client/Index");
        }
    }
}