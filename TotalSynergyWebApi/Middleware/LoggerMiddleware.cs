using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TotalSynergyWebApi.Middleware
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            //loggerFactory.AddFile($"Logs/myapp-{DateTime.Now.ToString()}.txt");
            _logger = loggerFactory.CreateLogger<LoggerMiddleware>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            //Read body from the request and log it
            using (var reader = new StreamReader(httpContext.Request.Body))
            {
                var requestBody = reader.ReadToEnd();
                //loggerFactory.AddFile("Logs/myapp-{Date}.txt");
                //As this is a middleware below line will make sure it will log each and every request body
                _logger.LogInformation(requestBody);
            }

            // log response for error handleing
            using (var responseBody = new MemoryStream())
            {
                await _next.Invoke(httpContext);

                var response = await FormatResponse(httpContext.Response);
                //    //if (!response.StartsWith("20")) {
                //    //    _logger.LogError(response);

                //    //}
            }
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {

            response.Body.Seek(0, SeekOrigin.Begin);
            string text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"{response.StatusCode}: {text}";
        }
    }
}