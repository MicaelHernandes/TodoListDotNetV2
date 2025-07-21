using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.DTOs.Request.User;
using Todo.Application.DTOs.Response;
using Todo.Application.DTOs.Response.User;
using Todo.Application.Exceptions.User;
using Todo.Application.UseCases.User;

namespace Todo.API.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Get(
            [FromServices] CreateUserUseCase useCase,
            [FromBody] CreateUserRequest request
        )
        {
            var result = await useCase.Execute(request);
            return StatusCode(StatusCodes.Status201Created, new ApiResponse<UserResponse>(result));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Auth(
            [FromServices] UserAuthUseCase useCase,
            [FromBody] UserAuthRequest request
        )
        {
            var result = await useCase.Execute(request);
            return StatusCode(StatusCodes.Status200OK, new ApiResponse<UserAuthResponse>(result));
        }
    }
}