using Todo.Application.DTOs.Request.Task;
using Todo.Application.DTOs.Response.Task;
using Todo.Application.Exceptions;
using Todo.Domain.Repositories;

namespace Todo.Application.UseCases.Task;

public class UpdatePartialTaskUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ITaskRepository _taskRepository;
    
    public UpdatePartialTaskUseCase(IUserRepository userRepository, ITaskRepository taskRepository)
    {
        _userRepository = userRepository;
        _taskRepository = taskRepository;
    }

    public async Task<ListTasksResponse> Execute(int idTask, UpdatePartialTaskRequest request, int userId)
    {
        var task = await _taskRepository.GetById(idTask);
        if (task == null)
        {
            throw new NotFoundException("Tarefa não encontrada ou não pertence ao usuário.");
        }
        
        var user = await  _userRepository.FindById(userId);
        if (user == null)
        {
            throw new NotFoundException("Usuário não encontrado.");
        }
        
        if (task.UserId != userId)
        {
            throw new ForbiddenRequestException("Usuário não autorizado a atualizar esta tarefa.");
        }
        
        task.UpdatePartial(
            request.Title,
            request.Description,
            request.TotalPomodori,
            request.PomodoroValue,
            request.CompletedPomodori,
            request.Status,
            request.TaskDate,
            request.DueDate
        );

        var newTask = await _taskRepository.Update(task);
        return new ListTasksResponse
        {
            Id = newTask.Id,
            UserId = newTask.UserId,
            Title = newTask.Title,
            Description = newTask.Description,
            TotalPomodori = newTask.TotalPomodori,
            PomodoroValue = newTask.PomodoroValue,
            CompletedPomodori = newTask.CompletedPomodori,
            Status = newTask.Status.ToString(),
            TaskDate = newTask.TaskDate,
            DueDate = newTask.DueDate,
            CompletedAt = newTask.CompletedAt
        };
    }
}