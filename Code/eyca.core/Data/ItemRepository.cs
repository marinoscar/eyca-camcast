using eyca.core.Models;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eyca.core.Data
{
    public class ItemRepository
    {

        public ItemRepository(ITableClient<Item> client)
        {
            Client = client;
        }

        public ITableClient<Item> Client { get; private set; }

        public IEnumerable<Item> GetActiveItems()
        {
            var filter = TableQuery.GenerateFilterConditionForBool("IsEnabled", "equals", true);
            var res = Client.RunQuery(new TableQuery().Where(filter)).ToList();
            return res.Select(i => new Item()
            {
                Id = i.RowKey,
                Type = i.PartitionKey,
                IsEnabled = i["IsEnabled"].BooleanValue.Value,
                Value = i["Value"].StringValue,
                RowKey = i.RowKey,
                PartitionKey = i.PartitionKey
            });
        }

        public void InsertOrReplace(Item item)
        {
            Client.InsertOrReplace(item);
        }



    }
}
