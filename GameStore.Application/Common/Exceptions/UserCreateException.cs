namespace GameStore.Application.Common.Exceptions;

public class UserCreateException : Exception
{
    public UserCreateException(string user, string description)
        : base($"Failed to create user '{user}'. Description: {description}")
    {
    }
}