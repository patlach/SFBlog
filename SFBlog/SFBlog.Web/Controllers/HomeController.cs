using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFBlog.BLL.Services.IServices;

namespace SFBlog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService, IMapper mapper)
        {
            this.homeService = homeService;
        }

        public async Task<ActionResult> Index()
        {
            return RedirectToAction("GetPosts", "Post");
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Home/Create")]
        public async Task<ActionResult> Create()
        {
            await this.homeService.GenerateData();
            return View();
        }
    }
}
