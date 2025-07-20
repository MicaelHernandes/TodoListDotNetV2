namespace Todo.Application.Exceptions.Task;

public class CreateTaskInvalidParametersException : Exception
{
    public CreateTaskInvalidParametersException(string message) : base(message) {}
}