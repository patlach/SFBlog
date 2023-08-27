using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFBlog.BLL.Services.IServices;
using NLog;

namespace SFBlog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;
        private readonly ILogger<HomeController> logger;

        public HomeController(IHomeService homeService, IMapper mapper, ILogger<HomeController> logger)
        {
            this.homeService = homeService;
            this.logger = logger;
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

            this.logger.LogInformation("Пользователи системы(тестовые) был созданы.");

            return View();
        }
    }
}
