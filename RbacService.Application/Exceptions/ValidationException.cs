using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Application.Exceptions
{
    [Serializable]
    public class ValidationException(IEnumerable<string> errors) : Exception(BuildMessage(errors))
    {
        public IReadOnlyList<string> Errors { get; } = errors.ToList().AsReadOnly();

        private static string BuildMessage(IEnumerable<string> errors)
        {
            if (errors == null || !errors.Any())
                return "Validation failed with unknown errors.";

            return $"Validation failed with {errors.Count()} error(s): {string.Join("; ", errors)}";
        }

        public override string ToString()
        {
            return $"{Message}{Environment.NewLine}{string.Join(Environment.NewLine, Errors)}";
        }
    }
}
