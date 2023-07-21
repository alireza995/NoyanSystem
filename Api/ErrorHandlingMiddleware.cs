using Basic.Exceptions;
using FluentValidation;
using Shared.Exception;

namespace Api;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next
    )
    {
        try
        {
            await next(context);
        }
        catch (BaseException exception)
        {
            await HandleCustomException(exception, context);
        }
        catch (ValidationException exception)
        {
            await HandleException(
                context,
                string.Join(Environment.NewLine, exception.Errors.Select(i => i.ErrorMessage)),
                400
            );
        }
        catch (Exception exception)
        {
            await HandleUnhandledException(exception, context);
        }
    }

    private async Task HandleCustomException(
        BaseException exception,
        HttpContext context
    )
    {
        _logger.LogError(
            new EventId(exception.EventId, exception.GetType().Name),
            exception,
            exception.LogPhrase
        );

        await HandleException(
            context,
            exception.Message,
            exception.StatusCode
        );
    }

    private async Task HandleUnhandledException(
        Exception exception,
        HttpContext context
    )
    {
        _logger.LogCritical(
            new EventId(),
            exception,
            exception.Message
        );
        await HandleException(
            context,
            "متاسفانه وب سایت ما با خطایی مواجه شده است که هنوز برطرف نشده است. " +
            "لطفاً با تیم پشتیبانی ما تماس بگیرید تا بتوانیم به شما کمک کنیم. " +
            "با عرض پوزش برای هر اختلالی که ایجاد کرده‌ایم.",
            500
        );
    }

    private async Task HandleException(
        HttpContext context,
        string message,
        int statusCode
    )
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(
            new ErrorDetails
            {
                StatusCode = statusCode,
                Message = message
            }
        );
    }
}