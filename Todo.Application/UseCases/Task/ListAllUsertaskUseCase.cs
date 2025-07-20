using Todo.Application.DTOs.Response.Task;
using Todo.Application.Exceptions.Task;
using Todo.Domain.Repositories;

namespace Todo.Application.UseCases.Task;

public class ListAllUsertaskUseCase
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    
    public ListAllUsertaskUseCase(ITaskRepository taskRepository, IUserRepository userRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
    }
    
    public async Task<List<ListTasksResponse>> ExecuteAsync(int userId)
    {
        var user = await _userRepository.FindById(userId);

        if (user == null)
        {
            throw new CreateTaskInvalidParametersException("Usuário não encontrado.");
        }

        var tasks = await _taskRepository.GetAllByUserId(user.Id);

        var response = tasks.Select(task => new ListTasksResponse
        {
            Id = task.Id,
            UserId = task.UserId,
            Title = task.Title,
            Description = task.Description,
            TotalPomodori = task.TotalPomodori,
            PomodoroValue = task.PomodoroValue,
            CompletedPomodori = task.CompletedPomodori,
            Status = task.Status.ToString(),
            TaskDate = task.TaskDate,
            DueDate = task.DueDate,
            CompletedAt = task.CompletedAt
        }).ToList();

        return response;
    }
}