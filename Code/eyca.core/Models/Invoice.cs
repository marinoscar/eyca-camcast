using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eyca.core.Models
{
    public class Invoice : Contact
    {
        public string ImageId { get; set; }

        public static Invoice InvoiceFromItem(Item item)
        {
            var invoice = JsonConvert.DeserializeObject<Invoice>(item.Value);
            invoice.Id = item.Id;
            invoice.IsDisabled = item.IsDisabled;
            return invoice;
        }

        protected override string GetItemType()
        {
            return "Invoice";
        }
    }
}
