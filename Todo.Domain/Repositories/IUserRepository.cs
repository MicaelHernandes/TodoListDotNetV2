using Todo.Domain.Entities;

namespace Todo.Domain.Repositories;

public interface IUserRepository
{
    public Task<User> Create(User user);
    public Task<User> Update(User user);
    public Task<bool> Delete(User user);
    public Task<User> FindByEmail(string email);
    public Task<User> FindByUsername(string username);
    public Task<User> FindById(int id);
}