using Todo.Application.DTOs.Request.Task;
using Todo.Application.DTOs.Response.Task;
using Todo.Application.Exceptions.Task;
using Todo.Domain.Repositories;

namespace Todo.Application.UseCases.Task;

public class CreateUserTaskUseCase
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;

    public CreateUserTaskUseCase(ITaskRepository taskRepository, IUserRepository userRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
    }

    public async Task<CreateTaskResponse> Execute(CreateTaskRequest request, string userEmail)
    {
        var user = _userRepository.FindByEmail(userEmail);
        if (user == null)
        {
            throw new CreateTaskInvalidParametersException("Usuario não encontrado");
        }

        var task = new Domain.Entities.Task(user.Id, request.Title, request.Description, request.TotalPomodori,
            request.PomodoroValue, request.TaskDate, request.DueDate);
        
        var newTask = await _taskRepository.Add(task);
        return new CreateTaskResponse
        {
            Id = newTask.Id,
            Title = newTask.Title,
            Description = newTask.Description,
            TotalPomodori = newTask.TotalPomodori,
            PomodoroValue = newTask.PomodoroValue,
            CompletedPomodori = newTask.CompletedPomodori,
            TaskDate = newTask.TaskDate,
            DueDate = newTask.DueDate,
            Status = newTask.Status.ToString(),
            AssignedAt = newTask.AssignedAt ?? DateTime.Now,
        };
    }
}