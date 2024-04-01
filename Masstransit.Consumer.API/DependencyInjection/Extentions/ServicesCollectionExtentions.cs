using Masstransit.Consumer.API.DependencyInjection.Options;
using MassTransit;
using System.Reflection;
using MediatR;
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
                mt.AddConsumers(Assembly.GetExecutingAssembly());
                mt.UsingRabbitMq((context, bus) =>
                {
                    bus.Host(masstansitConfiguration.Host, masstansitConfiguration.VHost, h =>
                    {
                        h.Username(masstansitConfiguration.UserName);
                        h.Password(masstansitConfiguration.Password);
                    });
                    bus.ConfigureEndpoints(context);
                });
            });
            return services;
        }
    }
}
