using FluentValidation.Results;

namespace MinimalApiArchitecture.Application.Extensions;

public static class GeneralExtensions
{
    public static Dictionary<string, string[]> ToValidationProblems(this ValidationResult result) =>
        result.Errors
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
}
