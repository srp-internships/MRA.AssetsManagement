using FluentValidation.Results;

namespace MRA.AssetsManagement.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation errors occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(Dictionary<string, string[]> failures)
        : this()
    {
        Errors = failures;
    }

    public IDictionary<string, string[]> Errors { get; }
}