namespace Todo.Domain.Repositories;

public interface ITaskRepository
{
    public Task<Domain.Entities.Task> Add(Domain.Entities.Task task);
    public Task<Domain.Entities.Task> Update(Domain.Entities.Task task);
    public Task<bool> Delete(Domain.Entities.Task task);
    public Task<ICollection<Domain.Entities.Task>> GetAllByUserId(int userId);
    public Task<Domain.Entities.Task> GetById(int taskId);
}