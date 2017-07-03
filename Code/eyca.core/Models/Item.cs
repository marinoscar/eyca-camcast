using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eyca.core.Models
{
    public class Item : TableEntity
    {
        public Item()
        {
            Id = NewId();
        }

        public Item(string id, string type) : base()
        {
            Id = id;
            Type = type;
            RowKey = Id;
            PartitionKey = Type;
        }

        public Item(string type) : base(NewId(), type)
        {
        }

        public string Id { get; set; }
        public string Type { get; set; }
        public bool IsEnabled { get; set; }
        public string Value { get; set; }

        public static string NewId()
        {
            return Guid.NewGuid().ToString().ToLowerInvariant().Replace("-", "");
        }
    }
}
