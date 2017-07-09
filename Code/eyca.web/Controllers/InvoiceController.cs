using eyca.core.Data;
using eyca.core.Models;
using eyca.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eyca.web.Controllers
{
    public class InvoiceController : Controller
    {
        private ItemRepository _repo;

        public InvoiceController()
        {
            _repo = new ItemRepository(new TableClient<Item>("items"));
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || id == "undefined") id = "03";
            return View(new ImageInfo() { ImageId = id });
        }

        [AcceptVerbs("POST")]
        public ActionResult Process(Invoice invoice)
        {
            _repo.AddInvoice(invoice);
            return Redirect("/Home/Thanks");
        }

        public ActionResult List()
        {
            var invoices = _repo.GetActiveInvoices();
            return View(invoices);
        }

        public ActionResult UpdateAll(string ids)
        {
            var idList = ids.Split(",".ToArray()).ToList();
            _repo.UpdateAllInvoicesAsDisabled(idList);
            return RedirectToAction("List");
        }
    }
}