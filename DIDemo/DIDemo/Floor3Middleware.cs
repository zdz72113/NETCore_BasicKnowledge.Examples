using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIDemo
{
    public class Floor3Middleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("Floor3Middleware In");
            //Do Something
            //To FloorTwoMiddleware
            await next(context);
            //Do Something
            Console.WriteLine("Floor3Middleware Out");
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseFloor3Middleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Floor3Middleware>();
        }
    }
}
