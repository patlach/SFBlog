using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SFBlog.BLL.Services.IServices;
using SFBlog.BLL.ViewModel;
using SFBlog.DAL.Models;

namespace SFBlog.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        private readonly UserManager<User> userManager;

        public CommentController(ICommentService commentService, UserManager<User> userManager)
        {
            this.commentService = commentService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> GetComments()
        {
            var comments = await this.commentService.GetComments();
            return View(comments);
        }

        [HttpGet]
        [Route("Comment/CreateComment")]
        public IActionResult CreateComment(Guid postId)
        {
            var model = new CommentCreateViewModel() { PostId = postId };

            return View(model);
        }

        [HttpPost]
        [Route("Comment/CreateComment")]
        public async Task<IActionResult> CreateComment(CommentCreateViewModel model, Guid postId)
        {
            model.PostId = postId;

            var user = await this.userManager.FindByNameAsync(User?.Identity?.Name);

            this.commentService.CreateComment(model, new Guid(user.Id));

            return RedirectToAction("GetPosts", "Post");
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
                ModelState.AddModelError("", "Некорректные данные");

                return View(model);
            }
        }

        [HttpDelete]
        [Route("Comment/Remove")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            await this.commentService.DeleteComment(id);

            return RedirectToAction("GetPosts", "Post");
        }
    }
}
