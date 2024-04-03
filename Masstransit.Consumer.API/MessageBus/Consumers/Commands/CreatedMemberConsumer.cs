using Masstransit.Consumer.API.Abstractions;
using Masstransit.Contract.IntegartionEvents;
using MediatR;

namespace Masstransit.Consumer.API.MessageBus.Consumers.Commands
{
    public class CreatedMemberConsumer : Consumer<DomainEvent.CreateMemberCommand>
    {
        public CreatedMemberConsumer(ISender Sender) : base(Sender)
        {
        }
    }
}
