using System;
namespace Common.Messages
{
    public interface IDomainEvent : IMessage
    {
        Guid ObjectId { get; set; }
        string ObjectVersion { get; set; }
        string UserId { get; set; }
        string System { get; set; }
    }
}

