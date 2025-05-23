using System.Linq;
using FluentValidation.Results;

namespace MinimalApiArchitecture.Application.Common.Extensions;

public static class ValidationExtensions
{
    public static IDictionary<string, string[]> GetValidationProblems(this ValidationResult result)
    {
        return result.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
    }
}
