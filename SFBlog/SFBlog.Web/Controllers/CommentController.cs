using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SFBlog.BLL.Services.IServices;
using SFBlog.BLL.ViewModel;
using SFBlog.DAL.Models;
using NLog;

namespace SFBlog.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        private readonly UserManager<User> userManager;
        private readonly ILogger<CommentController> logger;

        public CommentController(ICommentService commentService, UserManager<User> userManager, ILogger<CommentController> logger)
        {
            this.commentService = commentService;
            this.userManager = userManager;
            this.logger = logger;
        }

        public async Task<IActionResult> GetComments()
        {
            var comments = await this.commentService.GetComments();
            return View(comments);
        }

        [HttpGet]
        [Route("Comment/CreateComment")]
        public IActionResult CreateComment(Guid id)
        {
            var model = new CommentCreateViewModel() { PostId = id };

            return View(model);
        }

        [HttpPost]
        [Route("Comment/CreateComment")]
        public async Task<IActionResult> CreateComment(CommentCreateViewModel model, Guid id, string name)
        {
            if (!string.IsNullOrEmpty(model.Text))
            {
                model.PostId = id;

                var user = await this.userManager.FindByNameAsync(name);

                model.User = user;

                this.commentService.CreateComment(model, new Guid(user.Id));

                return RedirectToAction("GetPosts", "Post");
            }
            else
            {
                return View(model);
            }
        }

        [Route("Comment/Edit")]
        [HttpGet]
        public async Task<IActionResult> EditComment(Guid id)
        {
            var view = await this.commentService.EditComment(id);

            return View(view);
        }

        [Route("Comment/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditComment(CommentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.commentService.EditComment(model, model.Id);

                return RedirectToAction("GetPosts", "Post");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Route("Comment/Remove")]
        public async Task<IActionResult> RemoveComment(Guid id)
        {
            await this.commentService.DeleteComment(id);

            return RedirectToAction("GetPosts", "Post");
        }
    }
}
