using Guide.NET.Middleware.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<MyMiddleware>();

var app = builder.Build();


/**
 *  app.Run()
 *  app.Run(async (HttpContext context) => {
 *      //code
 *  });
 *  -> The extension method called Run is used to execute a terminating/short-circuiting middleware
 *  that doesn't forward the request to the next middleware
 *
 */

// we must use the Use method instead.
// middleware 1
app.Use(async (HttpContext context, RequestDelegate next) => {

    context.Response.Headers["content-type"] = "text/html";
    
    await context.Response.WriteAsync("<h4>I'm in the first middleware</h4>");
    await next(context);
});

// middleware 2
/*app.Use(async (context, next) => {
    await context.Response.WriteAsync("<h4>I'm in the second middleware</h4>");
    await next(context);
});*/

// first approach
app.UseMiddleware<MyMiddleware>();

// or
/*app.UseMyCustomMiddleware();*/
app.UseConventionalMiddleware();

// UseWhen middleware
app.UseWhen(context => context.Request.Query.ContainsKey("username"),
    app => {
        app.Use(async (context, next) => {
            await context.Response.WriteAsync("Hello from Middlware branch conditionner");
            await next();
        });
    });


// middleware 3
app.Run(async (HttpContext context) => {
    await context.Response.WriteAsync("<h4>I'm in the third middleware</h4>");
});


/**
 * Middlewares order: 
 * -> app.UseExceptionHandler("/Error");
 * -> app.UseHsts();
 * -> app.UseHttpsRedirecion();
 * -> app.UseStaticFiles();
 * -> app.UseRouting();
 * -> app.UseCORS();
 * -> app.UseAuthentication();
 * -> app.UseAuthorization();
 * -> app.UseSession();
 * -> app.MapControllers();
 * --------------------------->>>   add custom middlewares
 * -> app.Run();
 */


app.Run();
