using Masstransit.Consumer.API.DependencyInjection.Options;
using MassTransit;
using System.Reflection;
using MediatR;
using Masstransit.Consumer.API.MessageBus.Consumers.Events;
using RabbitMQ.Client;
using Masstransit.Contract.IntegartionEvents;
using Masstransit.Contract.Constants;
using Masstransit.Consumer.API.MessageBus.Consumers.Commands;

namespace Masstransit.Consumer.API.DependencyInjection.Extentions
{
    public static class ServicesCollectionExtentions
    {
        public static IServiceCollection AddMediatr(this IServiceCollection services) => services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        public static IServiceCollection AddConfigurationMasstansitRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {

            var masstansitConfiguration = new MasstransitConfiguration();
            configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstansitConfiguration);
            services.AddMassTransit(mt =>
            {
                //mt.AddConsumers(Assembly.GetExecutingAssembly());
                mt.AddConsumer<SendSmsWhenReceivedSmsEventConsume>();
                mt.AddConsumer<CreatedMemberConsumer>();
                mt.UsingRabbitMq((context, bus) =>
                {
                    bus.Host(masstansitConfiguration.Host, masstansitConfiguration.VHost, h =>
                    {
                        h.Username(masstansitConfiguration.UserName);
                        h.Password(masstansitConfiguration.Password);
                    });
                    // bus.Durable = true;

                    bus.ReceiveEndpoint("SendSms", e =>
                    {
                        e.ConfigureConsumeTopology = false;
                        e.Consumer<SendSmsWhenReceivedSmsEventConsume>(context);
                        e.Bind("SendSmsWhenReceivedSmsEventConsume", x =>
                        {
                            x.Durable = false;
                            x.AutoDelete = true;
                            x.ExchangeType = ExchangeType.Topic;
                            x.RoutingKey = NoitificationType.sms;
                        });
                    });
                    bus.ReceiveEndpoint("CreatedMember", e =>
                    {
                        e.Consumer<CreatedMemberConsumer>(context);
                    }
                    );
                    // bus.ConfigureEndpoints(context);
                });
            });
            return services;
        }
    }
}
