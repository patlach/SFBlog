using Microsoft.AspNetCore.Mvc;
using NLog;

namespace SFBlog.Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<CommentController> logger;

        public ErrorController(ILogger<CommentController> logger)
        {
            this.logger = logger;
        }

        [HttpGet("/Error")]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                this.logger.LogError($"{statusCode.Value}");
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
