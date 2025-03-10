using DevStudy.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DevStudy.API.Filters;

public class ExceptionFilters : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.ExceptionHandled = true;

        var exception = context.Exception;

        var response = new Response();
        response.IsSuccessed = false;
        response.Message = exception.Message; // Set custom message from exception

        int statusCode;

        // Customize status code based on exception type
        if (exception is ArgumentException)
        {
            statusCode = StatusCodes.Status400BadRequest;
        }
        else if (exception is UnauthorizedAccessException)
        {
            statusCode = StatusCodes.Status401Unauthorized;
        }
        else if (exception is DirectoryNotFoundException)  
        {
            statusCode = StatusCodes.Status404NotFound;
        }
        else
        {
            statusCode = StatusCodes.Status500InternalServerError;
        }

        context.Result = new JsonResult(response)
        {
            StatusCode = statusCode
        };
    }
}

