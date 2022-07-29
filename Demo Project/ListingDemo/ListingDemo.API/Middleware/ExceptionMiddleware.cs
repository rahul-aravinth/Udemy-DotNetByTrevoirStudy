using ListingDemo.API.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace ListingDemo.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDeleg;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate requestDeleg, ILogger<ExceptionMiddleware> logger)
        {
            this._requestDeleg = requestDeleg;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDeleg(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in {httpContext.Request.Path}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            var errorDetails = new ErrorDetails
            {
                ErrorType = "Failure",
                ErrorMessage = ex.Message
            };

            switch (ex)
            {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    errorDetails.ErrorType = "Not Found";
                    break;
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    errorDetails.ErrorType = "Bad Request";
                    break;
                default:
                    break;
            }

            string response = JsonConvert.SerializeObject(errorDetails);
            httpContext.Response.StatusCode = (int)statusCode;
            return httpContext.Response.WriteAsync(response);
        }
    }
}
