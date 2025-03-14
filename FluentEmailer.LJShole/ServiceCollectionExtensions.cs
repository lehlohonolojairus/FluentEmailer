using FluentEmailer.LJShole.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FluentEmailer.LJShole
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFluentEmailer(this IServiceCollection services)
        {
            services.AddScoped<IFluentEmail, FluentEmail>();
            return services;
        }
    }
}
