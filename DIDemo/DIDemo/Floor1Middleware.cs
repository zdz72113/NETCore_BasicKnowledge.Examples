using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIDemo
{
    public class Floor1Middleware
    {
        private readonly RequestDelegate _next;

        public Floor1Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Floor1Middleware In");
            //Do Something
            //To FloorTwoMiddleware
            await _next(context);
            //Do Something
            Console.WriteLine("Floor1Middleware Out");
        }
    }

    public static class Floor1MiddlewareExtensions
    {
        public static IApplicationBuilder UseFloor1Middleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Floor1Middleware>();
        }
    }
}
