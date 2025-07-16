using Todo.Application.DTOs.Request.User;
using Todo.Application.DTOs.Response;
using Todo.Application.Services;
using Todo.Domain.Repositories;

namespace Todo.Application.UseCases.User;

public class CreateUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public CreateUserUseCase(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<UserResponse> Execute(CreateUserRequest request)
    {
        var hashedPassword = _passwordService.Hash(request.Password);
        var user = new Domain.Entities.User(request.Name, request.Email, hashedPassword);
        var newUser = await _userRepository.Create(user);
        return new UserResponse(newUser.Id, newUser.Name, newUser.Email);
    }
}