using Masstransit.Contract.IntegartionEvents;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Masstransit.Contract.IntegartionEvents.DomainEvent;

namespace Masstransit.Consumer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBus _bus;
        private readonly ISender _sender;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBus bus, ISender sender)
        {
            _logger = logger;
            _bus = bus;
            _sender = sender;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("hello");
            
            await _bus.Send(new DomainEvent.CreateMemberCommand()
            {
                Description = "CreateMemberCommand",
                Id=new Guid(),
                Name= "CreateMember",
                Timestamp= DateTime.Now,    
                TransactionId= Guid.NewGuid()
            });
            return Accepted();
        }
    }
}