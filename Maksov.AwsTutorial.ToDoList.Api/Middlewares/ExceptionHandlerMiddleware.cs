using System.Net;
using FluentValidation;

namespace Maksov.AwsTutorial.ToDoList.Api.Middlewares;

public sealed class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while processing the request.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            KeyNotFoundException => (HttpStatusCode.NotFound, "The requested resource was not found."),
            ValidationException validationException => 
                (HttpStatusCode.BadRequest, 
                    string.Join(Environment.NewLine, validationException.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"))),
            _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
        };


        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            statusCode = (int)statusCode,
            message = message
        };

        return context.Response.WriteAsJsonAsync(response);
    }
}