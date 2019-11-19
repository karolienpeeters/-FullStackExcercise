using FluentValidation;
using FullStack.BLL.Interfaces;
using FullStack.BLL.Models;
using FullStack.BLL.Services;
using FullStack.BLL.Validators;
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

        public static IServiceCollection AddValidatorsDto(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<LoginDto>, LoginDtoValidator>();
            services.AddSingleton<IValidator<UserDto>, UserDtoValidator>();
            services.AddSingleton<IValidator<CustomerDto>, CustomerDtoValidator>();

            return services;
        }
    }
}