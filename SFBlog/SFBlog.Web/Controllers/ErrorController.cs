using Microsoft.AspNetCore.Mvc;

namespace SFBlog.Web.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("/Error")]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                switch (statusCode.Value)
                {
                    case 404:
                        return View("NotFound");
                    case 403:
                        return View("AccessDenied");
                    default:
                        return View("SWW");
                }
            }

            return View("SWW");
        }
    }
}
