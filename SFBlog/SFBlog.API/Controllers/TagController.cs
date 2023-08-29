using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFBlog.BLL.Services.IServices;
using SFBlog.BLL.ViewModel;

namespace SFBlog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : Controller
    {
        private readonly ITagService tagService;

        public TagController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        /// <summary>
        /// Создать тег
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("CreateTag")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> CreateTag(TagCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.tagService.CreateTag(model);

                return StatusCode(201);
            }
            else
            {
                return StatusCode(204);
            }
        }

        /// <summary>
        /// Редактировать тег
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("EditTag")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> EditTag(TagEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.tagService.EditTag(model, model.Id);
                return StatusCode(201);
            }
            else
            {
                return StatusCode(204);
            }
        }

        /// <summary>
        /// Удалить тег
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("DeleteTag")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> RemoveTag(Guid id)
        {
            var tag = await this.tagService.GetTag(id);
            await this.tagService.DeleteTag(id);

            return StatusCode(201);
        }
    }
}
