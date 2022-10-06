using System;
using Azure;
using Azure.Data.Tables;
using Infrastructure.Data.QueryRepository;

namespace Infrastructure.Data.TableStorage
{
    public interface ITSContext
    {
        void Insert(ITableEntity entity, string tableName);
        IEnumerable<TTableEntity> GetTableDataById<TTableEntity>(Guid id, string tableName) where TTableEntity : class, ITableEntity, new();
    }
}

