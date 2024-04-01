using Masstransit.Contract.IntegartionEvents;
using MediatR;

namespace Masstransit.Consumer.API.Usecases.Events
{
    public class SendSmsEventConsumerHandler : IRequestHandler<DomainEvent.SmsNotificationEvent>
    {
        private readonly ILogger<SendSmsEventConsumerHandler> _logger;
        public SendSmsEventConsumerHandler(ILogger<SendSmsEventConsumerHandler> logger)
        {
            _logger = logger;
        }
        public async Task Handle(DomainEvent.SmsNotificationEvent request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Message received: {message}", request);
        // throw new NotImplementedException();
        }
    }
}
