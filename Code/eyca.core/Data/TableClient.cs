using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace eyca.core.Data
{
    public class TableClient<T> : ITableClient<T> where T : TableEntity
    {
        public TableClient(string table) :
            this(table, new CloudStorageAccount(
                new StorageCredentials(ConfigurationManager.AppSettings["azure.storage.account"],
                    ConfigurationManager.AppSettings["azure.storage.key"]), true))
        {
        }

        public TableClient(string table, CloudStorageAccount account)
        {
            Account = account;
            Table = Account.CreateCloudTableClient().GetTableReference(table);
        }

        public CloudStorageAccount Account { get; private set; }
        public CloudTable Table { get; private set; }

        public void InsertOrReplace(T item)
        {
            var op = TableOperation.InsertOrReplace(item);
            Table.Execute(op);
        }

        public T GetById(string key, string partition)
        {
            var op = TableOperation.Retrieve<T>(partition, key);
            var res = Table.Execute(op);
            return (T)res.Result;
        }

        public IEnumerable<DynamicTableEntity> RunQuery(TableQuery query)
        {
            var res =  Table.ExecuteQuery(query).ToList();
            return res;
        }

    }
}
