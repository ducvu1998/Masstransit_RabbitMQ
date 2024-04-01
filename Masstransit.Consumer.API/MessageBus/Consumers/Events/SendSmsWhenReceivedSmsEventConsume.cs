using Masstransit.Consumer.API.Abstractions;
using Masstransit.Contract.IntegartionEvents;
using MediatR;

namespace Masstransit.Consumer.API.MessageBus.Consumers.Events
{
    public class SendSmsWhenReceivedSmsEventConsume : Consumer<DomainEvent.SmsNotificationEvent>
    {
        public SendSmsWhenReceivedSmsEventConsume(ISender Sender) : base(Sender)
        {
        }
    }
}
