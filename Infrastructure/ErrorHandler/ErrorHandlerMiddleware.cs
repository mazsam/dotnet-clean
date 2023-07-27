using Microsoft.AspNetCore.HostFiltering;
using Newtonsoft.Json;
using VendorBoilerplate.Application.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace VendorBoilerplate.Infrastructure.ErrorHandler
{
  public class ErrorHandlerMiddleware
  {
    private readonly RequestDelegate next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
      this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await next(context);
      } catch (Exception ex)
      {
        await HandleExceptionAsync(context, ex);
      }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
      var code = HttpStatusCode.InternalServerError;
      var errorMsg = ex.Message;
      if (ex is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;
      else if (ex is NotSupportedException) code = HttpStatusCode.PreconditionFailed;

      if (ex is ValidationException)
      {
        code = HttpStatusCode.BadRequest;
        errorMsg = string.Join("\n", ex.Message);
      }
      else if (ex is InterfaceException)
      {
        errorMsg = $"Terjadi kesalahan interface, {errorMsg}";
      }

      var result = JsonConvert.SerializeObject(new
      {
        success = false,
        message = errorMsg
      });

      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)code;
      return context.Response.WriteAsync(result);
    }
  }
}