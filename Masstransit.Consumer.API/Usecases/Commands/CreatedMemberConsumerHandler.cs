using Masstransit.Contract.IntegartionEvents;
using MassTransit;
using MediatR;

namespace Masstransit.Consumer.API.Usecases.Commands
{
    public class CreatedMemberConsumerHandler : IRequestHandler<DomainEvent.CreateMemberCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<CreatedMemberConsumerHandler> _logger;
        public CreatedMemberConsumerHandler(ILogger<CreatedMemberConsumerHandler> logger,IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }
        public async  Task Handle(DomainEvent.CreateMemberCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Message received: {message}", request);
            await _publishEndpoint.Publish(request);
            //throw new NotImplementedException();
        }
    }
}
