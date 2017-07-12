using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eyca.core.Models
{
    public class Client : Contact
    {
        public string Area { get; set; }

        protected override string GetItemType()
        {
            return "Client";
        }
    }
}
