using Guide.NET.ModelBinding.Models;
using Microsoft.AspNetCore.Mvc;

namespace Guide.NET.ModelBinding.Controllers;

public class HomeController : Controller
{
    [Route("bookstore/{bookid?}/{isloggedin?}")]
    public IActionResult Index([FromQuery] int? bookid, [FromRoute] bool? isloggedin, Book book)
    {
        /**
         * unorderd to accept null value, add ? after the data type of the parameter
         * we can specify from which data source we want to keep the value
         * [FromRoute] -> Data Route or [FromData] -> Query Parameters
         */
        if (bookid is null)
        {
            return BadRequest("Book id is not supplied or empty");
        }

        if(bookid <= 0)
        {
            return BadRequest("Book id can't be less than or equal to 0");
        }

        if(isloggedin.HasValue == false)
        {
            // return Unauthorized("User must be authenticated");
            return StatusCode(StatusCodes.Status401Unauthorized);
        }

        return Content($"Book id : {bookid}", "text/plain");
    }
}
