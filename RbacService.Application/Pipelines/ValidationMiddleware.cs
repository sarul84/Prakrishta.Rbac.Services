using RbacService.Application.Exceptions;
using RbacService.Application.Interfaces;
using Wolverine;

namespace RbacService.Application.Pipelines
{
    public class ValidationMiddleware<T>(IValidatorService<T> validatorService)
    {
        private readonly IValidatorService<T> _validatorService = validatorService;

        public async Task Handle(T message, IMessageContext context, Func<Task> next)
        {
            var result = await _validatorService.ValidateAsync(message);
            if (result.Any())
            {
                throw new ValidationException(result);
            }

            await next();
        }
    }
}
