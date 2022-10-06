using System;
using Azure;
using Azure.Data.Tables;

namespace Infrastructure.Data.QueryRepository
{
    public class EventStoreTE : TableEntityBase
    {
        public string Payload { get; set; }
        public string TypeName { get; set; }
    }
}

