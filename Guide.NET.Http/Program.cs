using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*app.MapGet("/", () => "Hello World!");*/

app.Run( async (HttpContext context) => {

    string path = context.Request.Path;
    string method = context.Request.Method;

    context.Response.Headers["newKey"] = "new value";
    context.Response.Headers["Content-Type"] = "text/html";

    await context.Response.WriteAsync("<h1> Hello </h1>");
    await context.Response.WriteAsync($"<h3> path : {path} </h3>");
    await context.Response.WriteAsync($"<h3> method : {method} </h3>");

    // Http Query String : /dashboard?"id=1"
    if(context.Request.Method == "GET") {
        if (context.Request.Query.ContainsKey("id")) {
            context.Response.StatusCode = 200; // OK
            var id = context.Request.Query["id"];
            await context.Response.WriteAsync(id);
        } else {
            context.Response.StatusCode = 400; // bad request
            await context.Response.WriteAsync("<h3> u must add the id to the request</h3>");
        }
    }

    // Http Request Header 
    if (context.Request.Headers.ContainsKey("User-Agent")) {
        string userAgent = context.Request.Headers["user-agent"];
        await context.Response.WriteAsync($"<h3>{userAgent}</h3>");
    }

    // Http Request Methods 
    /**
     * GET -> Requests to retrieve information (page, entity object or a static file)
     * POST -> Sends an entity object to server; generally it will be inserted into the database;
     * PUT -> Sends an entity object to server; generally updates all properties(full update) in the database
     * PATCH -> Sends an entity object to server; generally updates few properties (partial-update) in the database.
     * DELETE -> Requests to delete an entity in the database
     */

    if(context.Request.Method == "POST") {
        StreamReader streamReader = new StreamReader(context.Request.Body);
        string body = await streamReader.ReadToEndAsync();

        Dictionary<string, StringValues> queryDict = QueryHelpers.ParseQuery(body);

        if (queryDict.ContainsKey("name")) {
            string name = queryDict["name"];
            await context.Response.WriteAsync($"<p>{name}</p>");
        }
    }

});

app.Run();
