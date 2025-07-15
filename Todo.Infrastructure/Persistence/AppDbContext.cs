using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Task = Todo.Domain.Entities.Task;

namespace Todo.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }
}