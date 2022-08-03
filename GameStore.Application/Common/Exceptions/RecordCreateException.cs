using Microsoft.AspNetCore.Identity;

namespace GameStore.Application.Common.Exceptions;

public class RecordCreateException : Exception
{
    public IEnumerable<IdentityError>? Errors { get; private set; }
    
    public RecordCreateException(string message) : this(message, new List<IdentityError>())
    {
    }
    
    public RecordCreateException(IList<IdentityError> errors) : this(null, errors)
    {
        Errors = errors;
    }

    public RecordCreateException(string? message, IList<IdentityError> errors)
        : base(message != null ? $"{message} {BuildErrorMessage(errors)}" : BuildErrorMessage(errors))
    {
        Errors = errors;
    }
    
    private static string BuildErrorMessage(IEnumerable<IdentityError> errors) {
        var arr = errors.Select(x => $"{Environment.NewLine} -- {x.Code}: {x.Description}");
        return "Validation failed: " + string.Join(string.Empty, arr);
    }
}