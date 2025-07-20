using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.DTOs.Request.Task;
using Todo.Application.DTOs.Request.User;
using Todo.Application.DTOs.Response;
using Todo.Application.DTOs.Response.Task;
using Todo.Application.Exceptions;
using Todo.Application.Exceptions.Task;
using Todo.Application.UseCases.Task;

namespace Todo.API.Controllers
{
    [Route("api/v1/tasks")]
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
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var response = await useCase.Execute(request, userId);
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
        
        [HttpGet("list")]
        public async Task<IActionResult> List(
            [FromServices] ListAllUsertaskUseCase useCase
            )
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var response = await useCase.ExecuteAsync(userId);
                return Ok(new ApiResponse<List<ListTasksResponse>>(response));
                return Ok();
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

        [HttpGet("{idTask:int}")]
        public async Task<IActionResult> Get(
            [FromServices] GetTaskUseCase useCase,
            [FromRoute] int idTask
            )
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var response = await useCase.Execute(idTask, userId);
                return StatusCode(StatusCodes.Status200OK,new ApiResponse<ListTasksResponse>(response));
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<string>(ex.Message));
            }
            catch (ForbiddenRequestException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ApiResponse<string>(ex.Message));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<string>(e.Message));
            }
        }
    }
}
