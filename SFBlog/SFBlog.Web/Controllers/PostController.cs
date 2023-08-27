using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SFBlog.BLL.Services;
using SFBlog.BLL.Services.IServices;
using SFBlog.BLL.ViewModel;
using SFBlog.DAL.Models;
using NLog;

namespace SFBlog.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly IUserService userService;
        private readonly ITagService tagService;
        private readonly ICommentService commentService;
        private readonly UserManager<User> userManager;
        private readonly ILogger<PostController> logger;

        public PostController(IPostService postService, UserManager<User> userManager, ICommentService commentService, ILogger<PostController> logger)
        {
            this.postService = postService;
            this.userManager = userManager;
            this.commentService = commentService;
            this.logger = logger;
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
        public async Task<IActionResult> CreatePost(PostCreateViewModel model, string name)
        {
            if (ModelState.IsValid)
            {
                await this.postService.CreatePost(model, name);
                this.logger.LogInformation($"{model.Title} был создан");
                return RedirectToAction("GetPosts", "Post");
            }
            else
            {
                return View(model);
            }
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
            if (!string.IsNullOrEmpty(model.Title) && !string.IsNullOrEmpty(model.Text))
            {
                await this.postService.EditPost(model, Id);
                this.logger.LogInformation($"{model.Title} был отредактирован");
                return RedirectToAction("GetPosts", "Post");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Route("Post/Remove")]
        public async Task<IActionResult> RemovePost(Guid id)
        {
            await this.postService.DeletePost(id);
            this.logger.LogInformation($"Пост с {id} был удален");
            return RedirectToAction("GetPosts", "Post");
        }

        [HttpGet]
        [Route("Post/Get")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await this.postService.GetAllPosts();
            var users = this.userManager.Users.ToList();
            List<PostsViewModel> postsList = new List<PostsViewModel>();

            foreach (var post in posts)
            {
                postsList.Add(new PostsViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Text = post.Text,
                    User = users.Where(x => x.Id.ToLower() == post.Author.ToString().ToLower()).FirstOrDefault(),
                });
            }

            return View(postsList);
        }

        [HttpGet]
        [Route("Post/Details")]
        public async Task<IActionResult> GetPost(Guid id)
        {
            var post = await this.postService.GetPost(id);
            var user = this.userManager.Users.Where(x => x.Id.ToLower() == post.Author.ToString().ToLower()).FirstOrDefault();
            var comments = await this.commentService.GetCommentByPostId(id);

            PostViewModel postViewModel = new PostViewModel
            {
                Id = post.Id,
                Text = post.Text,
                Title = post.Title,
                User = user,
                Tags = post.Tags,
                Comment = comments
            };

            return View(postViewModel);
        }
    }
}
