using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Exceptions;

namespace Shop.Api.Exceptions;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IProblemDetailsService problemDetailsService)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
            exception,
            "Unhandled exception occurred. Path: {Path}",
            context.Request.Path);

        ProblemDetails problemDetails = exception switch
        {
            NotFoundException => CreateProblemDetails(
                StatusCodes.Status404NotFound,
                "Resource Not Found",
                exception.Message,
                context),

            ValidationException => CreateProblemDetails(
                StatusCodes.Status400BadRequest,
                "Validation Error",
                exception.Message,
                context),

            _ => CreateProblemDetails(
                StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                "An unexpected error occurred.",
                context)
        };

        context.Response.StatusCode = problemDetails.Status!.Value;

        await problemDetailsService.WriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = context,
                ProblemDetails = problemDetails
            });

        return true;
    }

    private static ProblemDetails CreateProblemDetails(
        int statusCode,
        string title,
        string detail,
        HttpContext context)
    {
        return new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };
    }
}