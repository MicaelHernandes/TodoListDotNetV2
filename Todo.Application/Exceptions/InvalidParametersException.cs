namespace Todo.Application.Exceptions;

public class InvalidParametersException : Exception
{
    public InvalidParametersException(string message) : base(message) {}
}