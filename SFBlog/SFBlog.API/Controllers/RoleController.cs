using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFBlog.BLL.Services.IServices;
using SFBlog.BLL.ViewModel;

namespace SFBlog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        /// <summary>
        /// Создать роль
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("CreateRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(RoleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.roleService.CreateRole(model);

                return StatusCode(201);
            }
            else
            {
                return StatusCode(204);
            }
        }

        /// <summary>
        /// Редактировать роль
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("EditRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(RoleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.roleService.EditRole(model);

                return StatusCode(201);
            }
            else
            {
                return StatusCode(204);
            }
        }

        /// <summary>
        /// Удалить роль
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("RemoveRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRole(Guid id)
        {
            await this.roleService.RemoveRole(id);

            return StatusCode(201);
        }

        /// <summary>
        /// Получить все роли
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("GetRoles")]
        [Authorize(Roles = "Admin")]
        public async Task<List<RolesViewModel>> GetRoles()
        {
            var roles = await this.roleService.GetAllRoles();

            return roles;
        }
    }
}
