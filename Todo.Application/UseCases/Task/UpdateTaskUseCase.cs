using Todo.Application.DTOs.Request.Task;
using Todo.Application.DTOs.Response.Task;
using Todo.Application.Exceptions;
using Todo.Domain.Repositories;

namespace Todo.Application.UseCases.Task;

public class UpdateTaskUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ITaskRepository _taskRepository;
    
    public UpdateTaskUseCase(IUserRepository userRepository, ITaskRepository taskRepository)
    {
        _userRepository = userRepository;
        _taskRepository = taskRepository;
    }

    public async Task<ListTasksResponse> Execute(int idTask, UpdateTaskRequest request, int userId)
    {
        var user = await _userRepository.FindById(userId);
        if (user == null)
        {
            throw new NotFoundException("Usuario da requisicao nao encontrado!");
        }
        
        var task = await _taskRepository.GetById(idTask);
        if (task == null)
        {
            throw new NotFoundException("Tarefa nao encontrada!");
        }
        
        if (task.UserId != userId)
        {
            throw new ForbiddenRequestException("Acesso negado: tarefa pertence a outro usuario!");
        }
        
        task.Update(request.Title, request.Description, request.TotalPomodori, request.PomodoroValue, request.CompletedPomodoro, request.Status, request.TaskDate, request.DueDate);
        
        await _taskRepository.Update(task);
        
        return new ListTasksResponse
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
        };
    }
}