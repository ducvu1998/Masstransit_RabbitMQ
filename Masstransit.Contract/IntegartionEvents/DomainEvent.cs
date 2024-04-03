using Masstransit.Contract.Abstractions.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masstransit.Contract.IntegartionEvents
{
    public static class DomainEvent
    {
        public record EmailNotificationEvent : INoitificationEvent
        {
            public Guid Id { get; set; }
            public DateTimeOffset Timestamp { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
            public Guid TransactionId { get; set; }
        }
        public record SmsNotificationEvent : INoitificationEvent
        {
            public Guid Id { get; set; }
            public DateTimeOffset Timestamp { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
            public Guid TransactionId { get; set; }
        }
        [ExcludeFromTopology]
        public record CreateMemberCommand : INoitificationEvent
        {
            public Guid Id { get; set; }
            public DateTimeOffset Timestamp { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public Guid TransactionId { get; set; }
        }

    }
}
//using Masstransit.Contract.Abtractions.Messages;
//namespace Masstransit.Contract.IntegartionEvents
//{
//    public static class DomainEvent
//    {
//        public record EmailNotificationEvent(

//            ) : INoitificationEvent
//        {
//            public Guid Id { get; set; }
//            public string Description { get; set; }
//            public string Name { get; set; }
//            public DateTimeOffset Timestamp { get; set; }
//            public Guid TransactionId { get; set; }
//            public string Type { get; set; }
//        }
//        public record SmsNotificationEvent(

//            ) : INoitificationEvent
//        {
//            public Guid Id { get; set; }
//            public string Description { get; set; }
//            public string Name { get; set; }
//            public DateTimeOffset Timestamp { get; set; }
//            public Guid TransactionId { get; set; }
//            public string Type { get; set; }
//        }
//    }
//}
