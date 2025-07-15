using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;
using Todo.Infrastructure.Persistence;

namespace Todo.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> Create(User user)
    {
        var newUser = _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User> Update(User user)
    {
        var updatedUser = _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return updatedUser.Entity;
    }

    public async Task<bool> Delete(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<User> FindByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<User> FindByUsername(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == username);
        return user;
    }
}