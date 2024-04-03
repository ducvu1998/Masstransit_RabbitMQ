using Masstransit.Consumer.API.Abstractions;
using Masstransit.Contract.IntegartionEvents;
using MediatR;

namespace Masstransit.Consumer.API.MessageBus.Consumers.Events
{
    public class SendEmailWhenReceivedEmailEventConsumer : Consumer<DomainEvent.EmailNotificationEvent>
    {
        public SendEmailWhenReceivedEmailEventConsumer(ISender Sender) : base(Sender)
        {
        }
    }
}
