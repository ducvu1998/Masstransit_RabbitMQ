using Masstransit.Contract.Abstractions.Messages;
using MassTransit;
using MediatR;

namespace Masstransit.Consumer.API.Abstractions
{
    public abstract class Consumer<TMessage> : IConsumer<TMessage>
        where TMessage : class,INoitificationEvent
    {
        private readonly ISender Sender;
        protected Consumer(ISender Sender)
        {
            this.Sender = Sender;
        }
        public async Task Consume(ConsumeContext<TMessage> context)
        {
            await Sender.Send(context.Message);
        }
    }
}
