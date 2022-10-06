using System;
using Azure;
using Azure.Data.Tables;

namespace Infrastructure.Data.QueryRepository
{
    public class TableEntityBase: ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}

