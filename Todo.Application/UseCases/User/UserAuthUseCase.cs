using Todo.Application.DTOs.Request.User;
using Todo.Application.DTOs.Response.User;
using Todo.Application.Exceptions.User;
using Todo.Application.Services;
using Todo.Domain.Repositories;
using Todo.Infrastructure.Auth;

namespace Todo.Application.UseCases.User;

public class UserAuthUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UserAuthUseCase(IUserRepository userRepository, IPasswordService passwordService, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<UserAuthResponse> Execute(UserAuthRequest request)
    {
        var user = await _userRepository.FindByEmail(request.Email);
        if (user == null)
        {
            throw new AuthUserInvalidCredentialsException("Credenciais invalidas!");
        }

        if (!_passwordService.Verify(user.Password, request.Password))
        {
            throw new AuthUserInvalidCredentialsException("Credenciais invalidas!");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new UserAuthResponse(token);
    }
}