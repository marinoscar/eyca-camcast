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

        public IEnumerable<Item> GetActiveItems(string type)
        {
            var filter = TableQuery.CombineFilters(TableQuery.GenerateFilterConditionForBool("IsDisabled", "eq", false), "and", TableQuery.GenerateFilterCondition("Type", "eq", type));
            var res = Client.RunQuery(new TableQuery().Where(filter)).ToList();
            return res.Select(i => new Item()
            {
                Id = i.RowKey,
                Type = i.PartitionKey,
                IsDisabled = i["IsDisabled"].BooleanValue.Value,
                Value = i["Value"].StringValue,
                RowKey = i.RowKey,
                PartitionKey = i.PartitionKey
            });
        }


        public void InsertOrReplace(Item item)
        {
            Client.InsertOrReplace(item);
        }

        public void AddContact(Contact contact)
        {
            contact.Id = Item.NewId();
            var item = contact.ToItem();
            InsertOrReplace(item);
        }

        public void AddClient(Client client)
        {
            client.Id = Item.NewId();
            var item = client.ToItem();
            InsertOrReplace(item);
        }

        public void AddInvoice(Invoice invoice)
        {
            invoice.Id = Item.NewId();
            var item = invoice.ToItem();
            InsertOrReplace(item);
        }

        public List<Contact> GetActiveContacts()
        {
            var items = GetActiveItems("Contact");
            return items.Select(i => Contact.ContactFromItem(i)).ToList();
        }

        public List<Invoice> GetActiveInvoices()
        {
            var items = GetActiveItems("Invoice");
            return items.Select(i => Invoice.InvoiceFromItem(i)).ToList();
        }


        public void UpdateAllContactsAsDisabled(List<string> ids)
        {
            UpdateAllItemsAsDisabled(ids, "Contact");
        }

        public void UpdateAllInvoicesAsDisabled(List<string> ids)
        {
            UpdateAllItemsAsDisabled(ids, "Invoice");
        }

        public void UpdateAllItemsAsDisabled(List<string> ids, string type)
        {
            foreach (var id in ids)
            {
                var item = Client.GetById(id, type);
                item.IsDisabled = true;
                InsertOrReplace(item);
            }
        }


    }
}
