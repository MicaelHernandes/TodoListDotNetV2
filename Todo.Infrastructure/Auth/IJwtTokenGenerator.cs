namespace Todo.Infrastructure.Auth;
using Todo.Domain.Entities;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}