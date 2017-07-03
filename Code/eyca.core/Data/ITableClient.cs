using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;

namespace eyca.core.Data
{
    public interface ITableClient<T> where T : TableEntity
    {
        T GetById(string key, string partition);
        void InsertOrReplace(T item);
        IEnumerable<DynamicTableEntity> RunQuery(TableQuery query);
    }
}