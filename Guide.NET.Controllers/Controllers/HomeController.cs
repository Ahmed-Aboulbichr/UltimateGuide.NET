using Guide.NET.Controllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Guide.NET.Controllers.Controllers;

// we can make this class as controller just by extending the name of the class by Controller
public class HomeController : Controller
{
    [Route("/")]
    [Route("/home")]
    public string Index()
    {
        return "Hello world";
    }

    [Route("expertise")]
    public ContentResult Expertise()
    {
        /*return new ContentResult() { Content = "Hello from expertise", ContentType = "text/plain" };*/

        /*return Content("Hello from expertise", "text/plain");*/

        return Content("<h1> Welcome </h1> <h3>Hello from Expertise</h3>", "text/html");
    }

    [Route("person")]
    public JsonResult Person()
    {
        Person person = new() { Id = Guid.NewGuid(), FirstName = "ABOULBICHR", LastName = "Ahmed", Age = 23 };
        return new JsonResult(person);
    }

    [Route("about")]
    public JsonResult About()
    {
        Person person = new() { Id = Guid.NewGuid(), FirstName = "ABOULBICHR", LastName = "Ahmed", Age = 23 };
        /*return new JsonResult(person);*/
        return Json(person); // much preferred
    }

    // File Results
    // File result sends the content of a file as response : pdf, txt, exe, zip ...
    [Route("file-download")]
    public VirtualFileResult FileDownload()
    {
        /*return new VirtualFileResult("/dummy.pdf", "application/pdf");*/
        return File("/dummy.pdf", "application/pdf");
    }

    [Route("file-download2")]
    public PhysicalFileResult FileDowload2()
    {
        /*return new PhysicalFileResult(@"C:\UltimateGuide.NET\Guide.NET.Controllers\wwwroot\dummy.pdf", "application/pdf");*/
        return PhysicalFile(@"C:\UltimateGuide.NET\Guide.NET.Controllers\wwwroot\dummy.pdf", "application/pdf");
    }

    // FileContentResult -> Represens a file from the byte[]
    // used when a part of the file or byte[] from other data source has to be sent as response.
    [Route("file-download2")]
    public FileContentResult FileDowload3()
    {
        byte[] bytes = System.IO.File.ReadAllBytes(@"C:\UltimateGuide.NET\Guide.NET.Controllers\wwwroot\dummy.pdf");
        /*return new FileContentResult(bytes, "application/pdf");*/
        return File(bytes, "application/pdf");
    }


    /**
     * IActionResult It is the parent interface for all action result classes such as ContentResult, JsonResult, RedirectResult, StatusCodeResult, ViewResult etc.
     * By mentioning the return type as IActionResult, you can return either of the subtypes of IActionResult
     */


    [Route("contact-us/{mobile:regex(^\\d{{10}}$)}")]
    public string Contact()
    {
        var num = 3;
        int.TryParse("str", out num);


        return "Hello from Contact";
    }


    
}
