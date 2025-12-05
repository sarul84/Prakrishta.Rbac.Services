using FluentValidation;
using JasperFx.Core.IoC;
using Microsoft.Extensions.DependencyInjection;
using RbacService.Application.Interfaces;
using RbacService.Application.Users.Commands;
using RbacService.Application.Validators;
using RbacService.Application.Validators.User;

namespace RbacService.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all FluentValidation validators and the generic ValidatorService
        /// for DTO validation. Consumers just call services.AddRbacValidators();
        /// </summary>
        public static IServiceCollection AddRbacValidators(this IServiceCollection services)
        {
            // This scans the assembly for ALL AbstractValidator<T> implementations
            // including those that inherit from UserValidatorBase<TCommand>
            services.AddValidatorsFromAssemblyContaining<UserValidatorBase<CreateUser>>();


            // Register the generic validator service for any command type
            services.AddScoped(typeof(IValidatorService<>), typeof(ValidatorService<>));

            return services;
        }
    }
}
