using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SFBlog.BLL.Services;
using SFBlog.BLL.Services.IServices;
using SFBlog.BLL.ViewModel;
using SFBlog.DAL.Models;

namespace SFBlog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly ICommentService commentService;
        private readonly UserManager<User> userManager;

        public PostController(IPostService postService, UserManager<User> userManager, ICommentService commentService)
        {
            this.postService = postService;
            this.userManager = userManager;
            this.commentService = commentService;
        }

        /// <summary>
        /// Создать пост
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("CreatePost")]
        public async Task<IActionResult> CreatePost(PostCreateViewModel model)
        {
            await this.postService.CreatePost(model, model.Author);

            return StatusCode(201);
        }

        /// <summary>
        /// Редактировать пост
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("EditPost")]
        public async Task<IActionResult> EditPost(PostEditViewModels model)
        {
            if (!string.IsNullOrEmpty(model.Title) && !string.IsNullOrEmpty(model.Text))
            {
                await this.postService.EditPost(model, model.Id);
                return StatusCode(201);
            }
            else
            {
                return StatusCode(204);
            }
        }
        
        /// <summary>
        /// Удалить пост
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("RemovePost")]
        public async Task<IActionResult> RemovePost(Guid id)
        {
            await this.postService.DeletePost(id);
            return StatusCode(201);
        }

        /// <summary>
        /// Получить все посты
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("GetPosts")]
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

        /// <summary>
        /// Получить пост
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("GetPost")]
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
