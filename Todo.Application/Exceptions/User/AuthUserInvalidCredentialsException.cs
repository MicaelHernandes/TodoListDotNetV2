namespace Todo.Application.Exceptions.User;

public class AuthUserInvalidCredentialsException : Exception
{
    public AuthUserInvalidCredentialsException(string message) : base(message)
    {
        
    }
}