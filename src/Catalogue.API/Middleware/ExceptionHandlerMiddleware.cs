using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;

namespace Catalogue.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(new EventId(ex.HResult),
                    ex,
                    ex.Message);

                var problemDetails = new ValidationProblemDetails()
                {
                    Instance = context.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = $"{ex.Message}: " + string.Join(", ", ex.Errors.Select(x => x.ErrorMessage))
                };

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(ex.HResult),
                    ex,
                    ex.Message);

                var json = new JsonErrorResponse
                {
                    Messages = new[] { "An error occur.Try it again." },
                    DeveloperMessage = ex
                };

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(json));
            }
        }

        private class JsonErrorResponse
        {
            public string[] Messages { get; set; }

            public object DeveloperMessage { get; set; }
        }
    }
}
