
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace VendorBoilerplate.Infrastructure.Authorization
{
  public class AuthMeMiddleware
  {
    private readonly IMemoryCache _memoryCache;
    private readonly RequestDelegate _next;

    public AuthMeMiddleware(RequestDelegate next, IMemoryCache memoryCache)
    {
      _memoryCache = memoryCache;
      _next = next;
    }

    public Task InvokeAsync(HttpContext context)
    {
      var authFilterContext = context.Request;
      var request = authFilterContext.HttpContext.Request;
      return _next.Invoke(context);
    }
  }
}