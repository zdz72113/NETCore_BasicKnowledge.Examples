using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogDemo
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {

                var Request = context.Request;
                ///访问路径
                string visit_url = Request.Path;
                ///URL 请求方法
                string method = Request.Method.ToUpper();
                ///URL 请求的参数
                string url_paramters = string.Empty;

                if (method == "GET") url_paramters = Request.QueryString.Value;


                if (method == "POST")
                {
                    foreach (var item in Request.Form)
                        url_paramters = url_paramters + item.Key + "=" + item.Value + "&";
                }

                ///错识信息
                string err_msg = ex.Message;//ex.StackTrace;

                ///日志格式内容
                var logs_msg = $"{visit_url}#{method}#{url_paramters}#{err_msg}";

                _logger.LogError(logs_msg);

                var statusCode = context.Response.StatusCode;

                var msg = $"Status Code: {statusCode}, Message: {ex.Message}";

                await HandleExceptionAsync(context, msg);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, string msg)
        {
            //var data = new Result { Title = "异常中间件返回", Msg = msg };
            //var result = JsonConvert.SerializeObject(data);
            //context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(msg);
        }
    }

    public static class GlobalErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalErrorHandlingMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalErrorHandlingMiddleware>();
        }
    }
}
