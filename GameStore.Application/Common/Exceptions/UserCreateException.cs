using Microsoft.AspNetCore.Identity;

namespace GameStore.Application.Common.Exceptions;

public class UserCreateException : Exception
{
    public UserCreateException(object existingFields) 
        : base($" Existing fields: {existingFields}")
    {
    }
    
    public UserCreateException(string user, string description)
        : base($"Failed to create user '{user}'. Description: {description}")
    {
    }

    public UserCreateException(IEnumerable<IdentityError> errors)
        : base(BuildErrorMessage(errors))
    {
    }
    
    private static string BuildErrorMessage(IEnumerable<IdentityError> errors) {
        var arr = errors.Select(x => $"{Environment.NewLine} -- {x.Code}: {x.Description}");
        return "Validation failed: " + string.Join(string.Empty, arr);
    }
}