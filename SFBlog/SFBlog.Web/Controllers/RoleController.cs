using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFBlog.BLL.Services.IServices;
using SFBlog.BLL.ViewModel;

namespace SFBlog.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        [Route("Role/Create")]
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [Route("Role/Create")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.roleService.CreateRole(model);

                return RedirectToAction("GetRoles", "Role");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");

                return View(model);
            }
        }

        [Route("Role/Edit")]
        [HttpGet]
        public async Task<IActionResult> EditRole(Guid id)
        {
            var role = this.roleService.GetRole(id);

            var view = new RoleEditViewModel { Id = id, Description = role.Result?.Description, Name = role.Result?.Name };

            return View(view);
        }

        [Route("Role/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditRole(RoleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.roleService.EditRole(model);

                return RedirectToAction("GetRoles", "Role");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");

                return View(model);
            }
        }

        [Route("Role/Remove")]
        [HttpPost]
        public async Task<IActionResult> RemoveRole(Guid id)
        {
            await this.roleService.RemoveRole(id);

            return RedirectToAction("GetRoles", "Role");
        }

        [Route("Role/GetRoles")]
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await this.roleService.GetRoles();

            return View(roles);
        }
    }
}
