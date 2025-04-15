using ConCorrenteDomain.Validation;
using System.Net;
using System.Text.Json;

namespace ConCorrente.WebAPI.Middlewares; 
public class ExceptionHandlingMiddleware {
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task Invoke(HttpContext context) {
        try {
            await _next(context);
        } catch (MovimentoEntityValidation ex) {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var result = JsonSerializer.Serialize(new {
                errorType = ex.ErrorType.ToString(),
                message = ex.Message
            });

            await context.Response.WriteAsync(result);
        } catch (Exception ex) {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var result = JsonSerializer.Serialize(new {
                error = "INTERNAL_ERROR",
                message = ex.Message
            });

            await context.Response.WriteAsync(result);
        }
    }
}
