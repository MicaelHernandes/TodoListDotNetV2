using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.DTOs.Request.Task;
using Todo.Application.DTOs.Request.User;
using Todo.Application.DTOs.Response;
using Todo.Application.DTOs.Response.Task;
using Todo.Application.Exceptions.Task;
using Todo.Application.UseCases.Task;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<CreateTaskResponse>),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromServices] CreateUserTaskUseCase useCase,
            [FromBody] CreateTaskRequest request
            )
        {
            try
            {
                var email = User.FindFirst("email")?.Value;
                var response = await useCase.Execute(request, email);
                return StatusCode(StatusCodes.Status201Created, new ApiResponse<CreateTaskResponse>(response));
            }
            catch (CreateTaskInvalidParametersException ex)
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
