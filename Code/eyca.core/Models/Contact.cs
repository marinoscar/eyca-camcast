using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eyca.core.Models
{
    public class Contact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsEnglish { get; set; }

        public static Contact FromItem(Item item)
        {
            var contact = JsonConvert.DeserializeObject<Contact>(item.Value);
            contact.Id = item.Id;
            contact.IsDisabled = item.IsDisabled;
            return contact;
        }

        public Item ToItem()
        {
            var type = "Contact";
            return new Item()
            {
                Type = type,
                Id = Id,
                IsDisabled = IsDisabled,
                RowKey = Id,
                PartitionKey = type,
                Value = JsonConvert.SerializeObject(this)
            };
        }

    }
}
