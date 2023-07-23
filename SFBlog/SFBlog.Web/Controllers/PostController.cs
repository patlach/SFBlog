using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SFBlog.BLL.Services.IServices;
using SFBlog.BLL.ViewModel;
using SFBlog.DAL.Models;

namespace SFBlog.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly UserManager<User> userManager;

        public PostController(IPostService postService, UserManager<User> userManager)
        {
            this.postService = postService;
            this.userManager = userManager;
        }

        [Route("Post/Create")]
        [HttpGet]
        public async Task<IActionResult> CreatePost()
        {
            var model = await this.postService.CreatePost();
            return View(model);
        }

        [Route("Post/Create")]
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostCreateViewModel model)
        {
            await this.postService.CreatePost(model);
            
            return RedirectToAction("GetPosts", "Post");
        }

        [Route("Post/Edit")]
        [HttpGet]
        public async Task<IActionResult> EditPost(Guid id)
        {
            var model = await this.postService.EditPost(id);

            return View(model);
        }

        [Route("Post/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditPost(PostEditViewModels model, Guid Id)
        {
            await this.postService.EditPost(model, Id);

            return RedirectToAction("GetPosts", "Post");
        }

        [HttpPost]
        [Route("Post/Remove")]
        public async Task<IActionResult> RemovePost(Guid id)
        {
            await this.postService.DeletePost(id);

            return RedirectToAction("GetPosts", "Post");
        }

        [HttpGet]
        [Route("Post/Get")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await this.postService.GetAllPosts();

            return View(posts);
        }
    }
}
