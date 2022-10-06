using System;
using System.Collections.Concurrent;
using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Common.Entities;
using Infrastructure.Data.QueryRepository;

namespace Infrastructure.Data.TableStorage
{
    public class TSContext : ITSContext
    {
        private readonly TableServiceClient tableServiceClient;

        public TSContext(TableServiceClient tableServiceClient)
        {
            this.tableServiceClient = tableServiceClient;
        }

        public void Insert(ITableEntity entity, string tableName)
        {
            tableServiceClient.CreateTableIfNotExists(tableName);

            var tableClient = this.tableServiceClient.GetTableClient(tableName);
            tableClient.AddEntity(entity);
        }

        public IEnumerable<TTableEntity> GetTableDataById<TTableEntity>(Guid id, string tableName) where TTableEntity : class, ITableEntity, new()
        {
            var tableClient = this.tableServiceClient.GetTableClient(tableName);

            return tableClient.Query<TTableEntity>(x => x.PartitionKey == id.ToString());
        }
    }
}
