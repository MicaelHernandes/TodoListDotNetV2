using Todo.Domain.Repositories;
using Todo.Infrastructure.Persistence;
using Task = Todo.Domain.Entities.Task;

namespace Todo.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Task> Add(Task task)
    {
        var newTask =  _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return newTask.Entity;
    }

    public async Task<Task> Update(Task task)
    {
        var updatedTask =  _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
        return updatedTask.Entity;
    }

    public async Task<bool> Delete(Task task)
    {
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}