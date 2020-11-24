using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebAPI.Extensions
{
    /// <summary>
    /// 自定义异常中间件类
    /// </summary>
    public class CustomizeExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomizeExceptionMiddleware> _logger;
        public CustomizeExceptionMiddleware(RequestDelegate next, ILogger<CustomizeExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError("异常错误未处理", ex);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //todo
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// 自定义异常中间件扩展类
    /// </summary>
    public static class CustomExceptionMiddlewareExtensions
    {
        /// <summary>
        /// 异常中间件扩展方法
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomizeExceptionMiddleware>();
        }
    }
}
