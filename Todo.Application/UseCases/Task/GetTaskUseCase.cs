using Todo.Application.DTOs.Response.Task;
using Todo.Application.Exceptions;
using Todo.Application.Exceptions.Task;
using Todo.Domain.Repositories;

namespace Todo.Application.UseCases.Task;

public class GetTaskUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ITaskRepository _taskRepository;
    
    public GetTaskUseCase(IUserRepository userRepository, ITaskRepository taskRepository)
    {
        _userRepository = userRepository;
        _taskRepository = taskRepository;
    }

    public async Task<ListTasksResponse> Execute(int idTask, int userId)
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