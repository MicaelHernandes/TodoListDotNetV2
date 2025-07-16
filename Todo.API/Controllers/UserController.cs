using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.DTOs.Request.User;
using Todo.Application.DTOs.Response;
using Todo.Application.Exceptions.User;
using Todo.Application.UseCases.User;

namespace Todo.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Get(
            [FromServices] CreateUserUseCase useCase,
            [FromBody] CreateUserRequest request
            )
        {
            try
            {
                var result = await useCase.Execute(request);
                return StatusCode(StatusCodes.Status201Created, new ApiResponse<UserResponse>(result));
            }
            catch (UserAlreadyExistsException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<string>(ex.Message));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>(e.Message));
            }
        }
    }
}
