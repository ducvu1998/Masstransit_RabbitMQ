using Masstransit.Contract.IntegartionEvents;
using Masstransit.Producer.API.DependencyInjection.Options;
using MassTransit;
using RabbitMQ.Client;

namespace Masstransit.Producer.API.DependencyInjection.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddConfigurationMasstansitRabbitMQ(this IServiceCollection services,IConfiguration configuration)
        {
            
            var masstansitConfiguration = new MasstransitConfiguration();
            configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstansitConfiguration);
            services.AddMassTransit(mt =>
            {
                mt.UsingRabbitMq((context, bus) =>
                {
                    bus.Host(masstansitConfiguration.Host, masstansitConfiguration.VHost, h =>
                    {
                        h.Username(masstansitConfiguration.UserName);
                        h.Password(masstansitConfiguration.Password);
                    });
                    bus.Send<DomainEvent.SmsNotificationEvent>(x =>
                    {
                        // use customerType for the routing key
                        x.UseRoutingKeyFormatter(context => context.Message.Type);

                        // multiple conventions can be set, in this case also CorrelationId
                      //  x.UseCorrelationId(context => context.Message.TransactionId);
                    });

                    bus.Message<DomainEvent.SmsNotificationEvent>(x => x.SetEntityName("SendSmsWhenReceivedSmsEventConsume"));

                    // Also if your publishing your message: because publishing a message will, by default, send it to a fanout queue.
                    // We specify that we are sending it to a direct queue instead. In order for the routingkeys to take effect.
                    bus.Publish<DomainEvent.SmsNotificationEvent>(x =>
                    {
                        x.Durable = false;
                        x.AutoDelete = true;
                        x.ExchangeType = ExchangeType.Topic;
                        
                    }) ;
                    //bus.Publish<DomainEvent.SmsNotificationEvent>(x =>
                    //{
                    //    x.Durable = false;
                    //    x.AutoDelete = true;
                    //    x.ExchangeType = ExchangeType.Topic;
                    //});

                });
            });
            return services;
        }
    }
}
