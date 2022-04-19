using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Products.Models;

namespace Products.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        IHostEnvironment env,
        ILogger<ExceptionMiddleware> logger
    )
    {
        _env = env;
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // catch any exception that may occur in the app
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // log the error message on the server
            _logger.LogError(ex, ex.Message);

            ApiException? response = null;

            // add more error details when environment is set to development
            // disable showing error details to client when env. is set to production

            if (_env.IsDevelopment())
            {
                response = new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace);
            }

            response ??= new ApiException(context.Response.StatusCode, "Sorry, an error occurred on the server.");

            //send error response to client

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.None
            }));
        }
    }
}