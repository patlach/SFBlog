using AutoMapper;
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
            await this.homeService.GenerateData();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Home/Error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
