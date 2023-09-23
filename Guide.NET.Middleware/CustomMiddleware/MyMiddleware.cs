namespace Guide.NET.Middleware.CustomMiddleware;
public class MyMiddleware : IMiddleware {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {

        await context.Response.WriteAsync("custom middleware -- Start");

        await next(context);

        await context.Response.WriteAsync("custom middleware -- End");

    }
}

public static class CustomMiddlewareExtension {
    public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app) {
        return app.UseMiddleware<MyMiddleware>();
    }
}
