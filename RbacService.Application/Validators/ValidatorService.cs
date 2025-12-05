using FluentValidation;
using RbacService.Application.Interfaces;

namespace RbacService.Application.Validators
{
    public class ValidatorService<T> : IValidatorService<T>
    {
        private readonly IValidator<T> _defaultValidator;
        private readonly RulesEngine.RulesEngine? _rulesEngine;

        public ValidatorService(IValidator<T> defaultValidator, RulesEngine.RulesEngine? rulesEngine = null)
        {
            _defaultValidator = defaultValidator;
            _rulesEngine = rulesEngine;
        }

        public async Task<IList<string>> ValidateAsync(T command, CancellationToken cancellationToken = default)
        {
            var errors = new List<string>();

            // Step 1: FluentValidation (baseline rules)
            var fluentResult = await _defaultValidator.ValidateAsync(command, cancellationToken);
            if (!fluentResult.IsValid)
                errors.AddRange(fluentResult.Errors.Select(e => e.ErrorMessage));

            // Step 2: RulesEngine (tenant-defined rules)
            if (_rulesEngine != null)
            {
                var workflowName = typeof(T).Name;
                var reResults = await _rulesEngine.ExecuteAllRulesAsync(workflowName, command);
                if (reResults != null && reResults.Any())
                {
                    foreach (var result in reResults.Where(r => !r.IsSuccess))
                    {
                        var errorMessage = result.Rule?.ErrorMessage ?? "Business rule validation failed";
                        errors.Add(errorMessage);
                    }
                }
            }

            return errors;
        }
    }
}
