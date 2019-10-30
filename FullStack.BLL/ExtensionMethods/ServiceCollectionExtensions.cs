using FullStack.BLL.Interfaces;
using FullStack.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FullStack.BLL.ExtensionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
