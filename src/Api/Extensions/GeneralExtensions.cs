using FluentValidation.Results;

namespace MinimalApiArchitecture.Api.Extensions;

public static class GeneralExtensions
{
    public static Dictionary<string, string[]> ToValidationProblems(this ValidationResult result) =>
        result.Errors
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
}
