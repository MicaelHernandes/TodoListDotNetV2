using Todo.Application.Exceptions;
using Todo.Domain.Repositories;

namespace Todo.Application.UseCases.Task;

public class DeleteTaskUseCase
{
    private readonly ITaskRepository _taskRepository;
    public DeleteTaskUseCase(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    
    public async Task<bool> ExecuteAsync(int taskId, int userId)
    {
        var task = await _taskRepository.GetById(taskId);
        if (task == null || task.UserId != userId)
        {
            throw new NotFoundException("Task não encontrada ou não pertence ao usuário.");
        }
        if (task.IsDeleted)
        {
            throw new InvalidParametersException("Task já foi excluída.");
        }
        if (task.Status == Domain.Enums.TaskStatus.Completed)
        {
            throw new InvalidParametersException("Task concluída não pode ser excluída.");
        }

        return await _taskRepository.Delete(task);
    }
}