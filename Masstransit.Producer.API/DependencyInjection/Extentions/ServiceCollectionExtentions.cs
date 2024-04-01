using Masstransit.Producer.API.DependencyInjection.Options;
using MassTransit;

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
                });
            });
            return services;
        }
    }
}
