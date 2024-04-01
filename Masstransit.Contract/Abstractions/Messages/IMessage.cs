using MassTransit;
using MediatR;

namespace Masstransit.Contract.Abstractions.Messages
{
    [ExcludeFromTopology]
    public interface IMessage:IRequest
    {
        public Guid Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
