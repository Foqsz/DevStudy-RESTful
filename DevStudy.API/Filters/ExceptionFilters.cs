using DevStudy.Domain.Models;
using DevStudy.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace DevStudy.API.Filters;

public class ExceptionFilters : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is GymExceptions)
            HandleProjectException(context);
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var exception = (GymExceptions)context.Exception;

        var response = new
        {
            Title = "Erro na aplicação",
            Status = 400,
            Message = exception.Message, 
        };

        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new JsonResult(response);
    }

}

