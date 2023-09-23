namespace Guide.NET.Middleware.CustomMiddleware; 
public class ConventionalMiddleware {

    private readonly RequestDelegate _next;

    public ConventionalMiddleware(RequestDelegate requestDelegate) {
        _next = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext context) {

        if(context.Request.Query.ContainsKey("firstname") && context.Request.Query.ContainsKey("lastname")) {
            var query = context.Request.Query;

            string fullname = $"{query["firstname"]} {query["firstname"]}";

            await context.Response.WriteAsync(fullname);
        }


        await _next(context);

        // after logic
    }
}

public static class ConventionalMiddlewareExtensions {

    public static IApplicationBuilder UseConventionalMiddleware(this IApplicationBuilder app) {

        return app.UseMiddleware<ConventionalMiddleware>();

    }

}
