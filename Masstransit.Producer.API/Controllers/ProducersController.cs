using Masstransit.Contract.Constants;
using Masstransit.Contract.IntegartionEvents;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using System.Xml.Linq;

namespace Masstransit.Producer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProducersController : ControllerBase
    {

        private readonly IPublishEndpoint _publishEndpoint;

        public ProducersController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost(Name = "publish-sms-noitification")]
        public async Task<IActionResult> PublishSmsNoitification()
        {
            //_logger.
            await _publishEndpoint.Publish(new DomainEvent.SmsNotificationEvent()
            {
                Id= Guid.NewGuid(),
                Description= "Sms description",
                Name= "sms noitification",
                Timestamp = DateTime.Now,
                TransactionId=Guid.NewGuid(),
                Type= NoitificationType.sms
            }
           );
            return Accepted();
        }
    }
}