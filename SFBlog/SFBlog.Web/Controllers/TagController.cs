using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFBlog.BLL.Services.IServices;
using SFBlog.BLL.ViewModel;

namespace SFBlog.Web.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService tagService;

        public TagController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        [Route("Tag/Create")]
        [HttpGet]
        public IActionResult CreateTag()
        {
            return View();
        }

        [Route("Tag/Create")]
        [HttpPost]
        public async Task<IActionResult> CreateTag(TagCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tagId = this.tagService.CreateTag(model);

                return RedirectToAction("GetTags", "Tag");
            }
            else
            {
                return View(model);
            }
        }

        [Route("Tag/Edit")]
        [HttpGet]
        public async Task<IActionResult> EditTag(Guid id)
        {
            var tag = await this.tagService.EditTag(id);
            return View(tag);
        }

        [Route("Tag/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditTag(TagEditViewModel model, Guid id)
        {
            if (ModelState.IsValid)
            {
                await this.tagService.EditTag(model, id);

                return RedirectToAction("GetTags", "Tag");
            }
            else
            {
                return View(model);
            }
        }

        [Route("Tag/Delete")]
        [HttpPost]
        public async Task<IActionResult> RemoveTag(Guid id)
        {
            var tag = await this.tagService.GetTag(id);
            await this.tagService.DeleteTag(id);

            return RedirectToAction("GetTags", "Tag");
        }

        [Route("Tag/Get")]
        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = await this.tagService.GetAllTags();

            return View(tags);
        }
    }
}
