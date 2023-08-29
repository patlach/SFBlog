using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SFBlog.BLL.Services.IServices;
using SFBlog.BLL.ViewModel;
using SFBlog.DAL.Models;

namespace SFBlog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService, UserManager<User> userManager)
        {
            this.commentService = commentService;
        }

        /// <summary>
        /// Добавить комментарий
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("CreateComment")]
        public async Task<IActionResult> CreateComment(CommentCreateViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Text))
            {
                this.commentService.CreateComment(model, new Guid(model.User.Id));

                return StatusCode(201);
            }
            else
            {
                return StatusCode(204);
            }
        }

        /// <summary>
        /// Редактировать комментарий
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("EditComment")]
        public async Task<IActionResult> EditComment(CommentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.commentService.EditComment(model, model.Id);

                return StatusCode(201);
            }
            else
            {
                return StatusCode(204);
            }
        }

        /// <summary>
        /// Удалить комментарий
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("RemoveComment")]
        public async Task<IActionResult> RemoveComment(Guid id)
        {
            await this.commentService.DeleteComment(id);

            return StatusCode(201);
        }
    }
}
