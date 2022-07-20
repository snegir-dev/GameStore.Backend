using Microsoft.AspNetCore.Identity;

namespace GameStore.Application.Common.Exceptions;

public class UserCreateException : Exception
{
    public IEnumerable<IdentityError>? Errors { get; private set; }
    
    public UserCreateException(string message) : this(message, new List<IdentityError>())
    {
    }
    
    public UserCreateException(IList<IdentityError> errors) : this(null, errors)
    {
        Errors = errors;
    }

    public UserCreateException(string? message, IList<IdentityError> errors)
        : base(message != null ? $"{message} {BuildErrorMessage(errors)}" : BuildErrorMessage(errors))
    {
        Errors = errors;
    }
    
    private static string BuildErrorMessage(IEnumerable<IdentityError> errors) {
        var arr = errors.Select(x => $"{Environment.NewLine} -- {x.Code}: {x.Description}");
        return "Validation failed: " + string.Join(string.Empty, arr);
    }
}