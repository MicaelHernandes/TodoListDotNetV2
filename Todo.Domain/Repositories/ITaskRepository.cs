namespace Todo.Domain.Repositories;

public interface ITaskRepository
{
    public Task<Domain.Entities.Task> Add(Task task);
    public Task<Domain.Entities.Task> Update(Task task);
    public Task<Domain.Entities.Task> Delete(Task task);
    public Task<Domain.Entities.Task> FindByEmail(string email);
    public Task<Domain.Entities.Task> FindByUsername(string username);
}