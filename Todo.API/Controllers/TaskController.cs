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
        
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<ListTasksResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
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
        
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CreateTaskResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
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

        [HttpGet("{idTask:int}")]
        [ProducesResponseType(typeof(ApiResponse<ListTasksResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
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
        
        [HttpPut("{idTask:int}")]
        [ProducesResponseType(typeof(ApiResponse<ListTasksResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            [FromServices] UpdateTaskUseCase useCase,
            [FromRoute] int idTask,
            [FromBody] UpdateTaskRequest request
        )
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var response = await useCase.Execute(idTask, request, userId);
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<ListTasksResponse>(response));
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<string>(ex.Message));
            }
            catch (ForbiddenRequestException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ApiResponse<string>(ex.Message));
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
        
        [HttpPatch("{idTask:int}")]
        [ProducesResponseType(typeof(ApiResponse<ListTasksResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePartial(
            [FromServices] UpdatePartialTaskUseCase useCase,
            [FromRoute] int idTask,
            [FromBody] UpdatePartialTaskRequest request
        )
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var response = await useCase.Execute(idTask, request, userId);
                return StatusCode(StatusCodes.Status200OK, new ApiResponse<ListTasksResponse>(response));
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<string>(ex.Message));
            }
            catch (ForbiddenRequestException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ApiResponse<string>(ex.Message));
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
        
        [HttpDelete("{idTask:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(
            [FromServices] DeleteTaskUseCase useCase,
            [FromRoute] int idTask
        )
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var response = await useCase.ExecuteAsync(idTask, userId);
                return StatusCode(StatusCodes.Status204NoContent, new ApiResponse<bool>(response));
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<string>(ex.Message));
            }
            catch (ForbiddenRequestException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ApiResponse<string>(ex.Message));
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
