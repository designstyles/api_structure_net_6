using Backend.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace WebApi.Middleware
{
    public class HttpResponseFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                var res = context.Result as ObjectResult;
                var responseResult = new ApiResponse<object>() { Error = null, StatusCode = res?.StatusCode ?? 400, Result = new List<object>(), Date = DateTime.Now };

                ObjectResult? controllerData;
                switch (res?.StatusCode)
                {
                    case 200:
                        controllerData = context.Result as OkObjectResult;
                        responseResult.Result = new List<object>() { controllerData?.Value ?? "" };
                        break;

                    case 201:
                        controllerData = context.Result as CreatedResult;
                        responseResult.Result = new List<object>() { controllerData?.Value ?? "" };
                        break;

                    case 400:
                        controllerData = context.Result as BadRequestObjectResult;
                        responseResult.Result = new List<object>();
                        responseResult.Error = new List<object>() { controllerData?.Value ?? "" };
                        break;

                    case 404:
                        controllerData = context.Result as NotFoundObjectResult;
                        responseResult.Result = new List<object>();
                        responseResult.Error = new List<object>() { controllerData?.Value ?? "" };
                        break;
                        //case 422:
                        //    controllerData = context.Result as ValidationFailedResult;
                        //    responseResult.Result = new List<object>();
                        //    responseResult.Error = new List<object>() { controllerData?.Value ?? "" };
                        //    break;
                }

                context.Result = new ObjectResult(string.Empty)
                {
                    StatusCode = res?.StatusCode ?? 400,
                    Value = responseResult,
                };
            }
            else if (context.Exception != null)
            {
                //set first, so that exception is handled, but overriden below if needed
                var errorResult = new ApiResponse<object>() { Error = new List<object>() { "Error has occured" }, StatusCode = (int)HttpStatusCode.BadRequest, Result = null, Date = DateTime.Now };

                if (context.Exception is Exception exception)
                {
                    errorResult.Error = new List<object>() { new ApiExceptionResponse(exception.Message) };
                    context.Result = new ObjectResult(exception.Message)
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Value = errorResult,
                    };

                    context.ExceptionHandled = true;
                }

            }
        }


    }
}
