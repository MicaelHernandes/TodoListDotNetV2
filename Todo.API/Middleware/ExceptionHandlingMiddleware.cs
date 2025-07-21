using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Todo.Application.DTOs.Response;
using Todo.Application.Exceptions;
using Todo.Application.Exceptions.Task;
using Todo.Application.Exceptions.User;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        int statusCode;
        string message = exception.Message;

        switch (exception)
        {
            case NotFoundException:
                statusCode = StatusCodes.Status404NotFound;
                break;
            case ForbiddenRequestException:
                statusCode = StatusCodes.Status403Forbidden;
                break;
            case CreateTaskInvalidParametersException:
                statusCode = StatusCodes.Status400BadRequest;
                break;
            case UserAlreadyExistsException:
                statusCode = StatusCodes.Status400BadRequest;
                break;
            case AuthUserInvalidCredentialsException:
                statusCode = StatusCodes.Status400BadRequest;
                break;
            default:
                statusCode = StatusCodes.Status500InternalServerError;
                message = "An unexpected error occurred.";
                break;
        }

        response.StatusCode = statusCode;

        var apiResponse = new ApiResponse<string>(message);

        var json = JsonSerializer.Serialize(apiResponse, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return response.WriteAsync(json);
    }
}