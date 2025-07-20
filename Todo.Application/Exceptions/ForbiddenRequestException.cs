namespace Todo.Application.Exceptions;

public class ForbiddenRequestException : Exception
{
    public ForbiddenRequestException(string message) : base(message) {}
}