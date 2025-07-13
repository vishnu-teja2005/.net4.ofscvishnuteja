using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Net;

namespace MyEmployeeWebApi.Filters // IMPORTANT: Ensure this namespace matches your project name + .Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;

        public CustomExceptionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;

            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "error_log.txt");
            string logMessage = $"{DateTime.Now}: {exception.Message}\nStackTrace: {exception.StackTrace}\n\n";

            try
            {
                File.AppendAllText(logFilePath, logMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging exception: {ex.Message}");
            }

            // While 'ExceptionResult' was mentioned, for ASP.NET Core,
            // ProblemDetails is the standard and recommended way to return API error details.
            // ExceptionResult (from WebApiCompatShim) is more aligned with older Web API behavior.
            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "An unexpected error occurred.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Detail = _env.IsDevelopment() ? exception.ToString() : "An internal server error has occurred. Please try again later."
            };

            context.Result = new ObjectResult(problemDetails)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}