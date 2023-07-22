using Microsoft.AspNetCore.Mvc;
using SFBlog.BLL.Services.IServices;
using SFBlog.BLL.ViewModel;

namespace SFBlog.Web.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
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
                var tagId = _tagService.CreateTag(model);

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
            var tag = await _tagService.EditTag(id);
            return View(tag);
        }

        [Route("Tag/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditTag(TagEditViewModel model, Guid id)
        {
            if (ModelState.IsValid)
            {
                await _tagService.EditTag(model, id);

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
            var tag = await _tagService.GetTag(id);
            await _tagService.DeleteTag(id);

            return RedirectToAction("GetTags", "Tag");
        }

        [Route("Tag/Get")]
        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _tagService.GetAllTags();

            return View(tags);
        }
    }
}
